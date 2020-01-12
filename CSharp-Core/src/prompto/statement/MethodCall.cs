using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.expression;
using prompto.grammar;
using prompto.declaration;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.param;


namespace prompto.statement
{

    public class MethodCall : SimpleStatement, IAssertion
    {

        MethodSelector selector;
        ArgumentList arguments;
        String variableName;

        public MethodCall(MethodSelector method)
        {
            this.selector = method;
        }

        public MethodCall(MethodSelector selector, ArgumentList arguments)
        {
            this.selector = selector;
            this.arguments = arguments;
        }

        public void setVariableName(String variableName)
        {
            this.variableName = variableName;
        }

        public override string ToString()
        {
            return selector.ToString() + (arguments != null ? arguments.ToString() : "");
        }

        public MethodSelector getSelector()
        {
            return selector;
        }

        public ArgumentList getArguments()
        {
            return arguments;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
            if (requiresInvoke(writer))
                writer.append("invoke: ");
            selector.ToDialect(writer);
            if (arguments != null)
                arguments.ToDialect(writer);
            else if (writer.getDialect() != Dialect.E)
                writer.append("()");
        }

        private bool requiresInvoke(CodeWriter writer)
        {
            if (writer.getDialect() != Dialect.E || (arguments != null && arguments.Count > 0))
                return false;
            try
            {
                MethodFinder finder = new MethodFinder(writer.getContext(), this);
                IMethodDeclaration method = finder.findMethod(false);
                /* if method is a reference */
                return method is AbstractMethodDeclaration || method.ClosureOf != null;
            }
            catch (SyntaxError /*e*/)
            {
                // ok
            }
            return false;
        }
        
        public override IType check(Context context)
        {
            MethodFinder finder = new MethodFinder(context, this);
            IMethodDeclaration declaration = finder.findMethod(false);
            Context local = IsLocalClosure(context) ? context : selector.newLocalCheckContext(context, declaration);
            return check(declaration, context, local);
        }


        private bool IsLocalClosure(Context context)
        {
            if (this.selector.getParent() != null)
                return false;
            IDeclaration decl = context.getLocalDeclaration<IDeclaration>(this.selector.getName());
            return decl is MethodDeclarationMap;
        }

        private IType check(IMethodDeclaration declaration, Context parent, Context local)
        {
            if (declaration.isTemplate())
                return fullCheck((ConcreteMethodDeclaration)declaration, parent, local);
            else
                return lightCheck(declaration, parent, local);
        }

        private IType lightCheck(IMethodDeclaration declaration, Context parent, Context local)
        {
            declaration.registerParameters(local);
            return declaration.check(local);
        }

        private IType fullCheck(ConcreteMethodDeclaration declaration, Context parent, Context local)
        {
            try
            {
                ArgumentList arguments = makeArguments(parent, declaration);
                declaration.registerParameters(local);
                foreach (Argument argument in arguments)
                {
                    IExpression expression = argument.resolve(local, declaration, true);
                    IValue value = argument.Parameter.checkValue(parent, expression);
                    local.setValue(argument.GetName(), value);
                }
                return declaration.check(local);
            }
            catch (PromptoError e)
            {
                throw new SyntaxError(e.Message);
            }
        }

        public ArgumentList makeArguments(Context context, IMethodDeclaration declaration)
        {
            if (arguments == null)
                return new ArgumentList();
            else
                return arguments.makeArguments(context, declaration);
        }

        
        public override IValue interpret(Context context)
        {
            IMethodDeclaration declaration = findDeclaration(context);
            Context local = selector.newLocalContext(context, declaration);
            declaration.registerParameters(local);
            registerArguments(context, local, declaration);
            return declaration.interpret(local);
        }

        private void registerArguments(Context context, Context local, IMethodDeclaration declaration)
        {
            ArgumentList arguments = makeArguments(context, declaration);
            foreach (Argument argument in arguments)
            {
                IExpression expression = argument.resolve(local, declaration, true);
                IParameter parameter = argument.Parameter;
                IValue value = parameter.checkValue(context, expression);
                if (value != null && parameter.isMutable() && !value.IsMutable())
                    throw new NotMutableError();
                local.setValue(argument.GetName(), value);
            }
        }

        public bool interpretAssert(Context context, TestMethodDeclaration testMethodDeclaration)
        {
            IValue value = this.interpret(context);
            if (value is prompto.value.BooleanValue)
                return ((prompto.value.BooleanValue)value).Value;
            else
            {
                CodeWriter writer = new CodeWriter(this.Dialect, context);
                this.ToDialect(writer);
                throw new SyntaxError("Cannot test '" + writer.ToString() + "'");
            }
        }

        private IMethodDeclaration findDeclaration(Context context)
        {
            try
            {
                Object o = context.getValue(selector.getName());
                if (o is ClosureValue)
                    return new ClosureDeclaration((ClosureValue)o);
                else if (o is ArrowValue)
                    return new ArrowDeclaration((ArrowValue)o);
            }
            catch (PromptoError)
            {
            }
            MethodFinder finder = new MethodFinder(context, this);
            return finder.findMethod(true);
        }


    }
}