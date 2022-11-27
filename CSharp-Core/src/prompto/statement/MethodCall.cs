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
using System.Collections.Generic;
using System.Linq;

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


        public override void ToDialect(CodeWriter writer)
        {
            if (requiresInvoke(writer))
                writer.append("invoke: ");
            selector.ToDialect(writer, false);
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
                IMethodDeclaration method = finder.findBest(false);
                /* if method is a reference */
                return method.isAbstract() || method.ClosureOf != null;
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
            IMethodDeclaration declaration = finder.findBest(false);
            if (declaration == null)
                return VoidType.Instance;
            if (declaration.isAbstract())
            {
                checkAbstractOnly(context, declaration);
                return declaration.getReturnType() != null ? declaration.getReturnType() : VoidType.Instance;
            }
            else
            {
                Context local = IsLocalClosure(context) ? context : selector.newLocalCheckContext(context, declaration);
                return checkDeclaration(declaration, context, local);
            }
        }

        private void checkAbstractOnly(Context context, IMethodDeclaration declaration)
        {
            if (declaration.IsReference()) // parameter or variable populated from a method call
                return;
            if (declaration.getMemberOf() != null) // the category could be subclassed (if constructor called on abstract, that would raise an error anyway)
                return;
            // if a global method, need to check for runtime dispatch
            MethodFinder finder = new MethodFinder(context, this);
            ISet<IMethodDeclaration> potential = finder.findPotential();
            if(potential.All(decl => decl.isAbstract()))
                throw new SyntaxError("Cannot call abstract method: " + declaration.getSignature(Dialect.O));
        }


        public override IType checkReference(Context context)
        {
            MethodFinder finder = new MethodFinder(context, this);
            IMethodDeclaration method = finder.findBest(false);
            if (method != null)
                return new MethodType(method);
            else
                return null;
        }

        private bool IsLocalClosure(Context context)
        {
            if (this.selector.getParent() != null)
                return false;
            IDeclaration decl = context.getLocalDeclaration<IDeclaration>(this.selector.getName());
            return decl is MethodDeclarationMap;
        }

        private IType checkDeclaration(IMethodDeclaration declaration, Context parent, Context local)
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
            MethodFinder finder = new MethodFinder(context, this);
            IMethodDeclaration declaration = finder.findBest(true);
            if (declaration == null)
                throw new SyntaxError("No such method: " + this.ToString());
            Context local = selector.newLocalContext(context, declaration);
            declaration.registerParameters(local);
            assignArguments(context, local, declaration);
            return declaration.interpret(local);
        }

        public override IValue interpretReference(Context context)
        {
            IMethodDeclaration declaration = findDeclaration(context);
            return new ClosureValue(context, new MethodType(declaration));
        }


        private void assignArguments(Context context, Context local, IMethodDeclaration declaration)
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
            IMethodDeclaration method = findRegistered(context);
            if (method != null)
                return method;
            else
            {
                MethodFinder finder = new MethodFinder(context, this);
                return finder.findBest(true);
            }
        }

        private IMethodDeclaration findRegistered(Context context)
        {
            if(selector.getParent()==null) try
            {
                Object o = context.getValue(selector.getName());
                if (o is ClosureValue)
                    return getClosureDeclaration(context, (ClosureValue)o);
                else if (o is ArrowValue)
                    return new ArrowDeclaration((ArrowValue)o);
            }
            catch (PromptoError)
            {
            }
            return null;
        }

        private IMethodDeclaration getClosureDeclaration(Context context, ClosureValue closure)
        {
            IMethodDeclaration decl = closure.Method;
            if (decl.getMemberOf() != null)
            {
                // the closure references a member method (useful when a method reference is needed)
                // in which case we may simply want to return that method to avoid spilling context into method body
                // this is only true if the closure comes straight from the method's instance context
                // if the closure comes from an accessible context that is not the instance context
                // then it is a local variable that needs the closure context to be interpreted
                Context declaring = context.contextForValue(selector.getName());
                if (declaring == closure.getContext())
                    return decl;
            }
            return new ClosureDeclaration(closure); throw new NotImplementedException();
        }
    }
}