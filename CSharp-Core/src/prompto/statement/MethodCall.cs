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
using prompto.argument;


namespace prompto.statement
{

	public class MethodCall : SimpleStatement, IAssertion
    {

		MethodSelector selector;
        ArgumentAssignmentList assignments;
		String variableName;

        public MethodCall(MethodSelector method)
        {
            this.selector = method;
        }

        public MethodCall(MethodSelector selector, ArgumentAssignmentList assignments)
        {
            this.selector = selector;
            this.assignments = assignments;
        }

		public void setVariableName(String variableName)
		{
			this.variableName = variableName;
		}

		public override string ToString ()
		{
			return selector.ToString() + (assignments != null ? assignments.ToString() : "");
		}

        public MethodSelector getSelector()
        {
            return selector;
        }

        public ArgumentAssignmentList getAssignments()
        {
            return assignments;
        }

		override
		public void ToDialect(CodeWriter writer) {
			if (requiresInvoke(writer))
				writer.append("invoke: ");
			selector.ToDialect(writer);
			if (assignments != null)
				assignments.ToDialect(writer);
			else if (writer.getDialect() != Dialect.E)
				writer.append("()");
		}

		private bool requiresInvoke(CodeWriter writer) {
			if (writer.getDialect() != Dialect.E)
				return false;
			if (assignments != null && assignments.Count > 0)
				return false;
			try {
				MethodFinder finder = new MethodFinder(writer.getContext(), this);
				IMethodDeclaration declaration = finder.findMethod(false);
				/* if method is abstract, need to prefix with invoke */
				if(declaration is AbstractMethodDeclaration)
					return true;
			} catch(SyntaxError /*e*/) {
				// ok
			}
			return false;
		}
        override
        public IType check(Context context)
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

		private IType lightCheck(IMethodDeclaration declaration,  Context parent, Context local)
        {
            declaration.registerArguments(local);
            return declaration.check(local);
        }

		private IType fullCheck(ConcreteMethodDeclaration declaration,  Context parent, Context local)
        {
            try
            {
				ArgumentAssignmentList assignments = makeAssignments(parent, declaration);
			 	declaration.registerArguments(local);
                foreach (ArgumentAssignment assignment in assignments)
                {
                    IExpression expression = assignment.resolve(local, declaration, true);
					IValue value = assignment.getArgument().checkValue(parent, expression);
					local.setValue(assignment.GetName(), value);
                }
                return declaration.check(local);
            }
            catch (PromptoError e)
            {
                throw new SyntaxError(e.Message);
            }
        }

        public ArgumentAssignmentList makeAssignments(Context context, IMethodDeclaration declaration)
        {
            if (assignments == null)
                return new ArgumentAssignmentList();
            else
                return assignments.makeAssignments(context, declaration);
        }

        override
        public IValue interpret(Context context)
        {
            IMethodDeclaration declaration = findDeclaration(context);
            Context local = selector.newLocalContext(context, declaration);
            declaration.registerArguments(local);
            ArgumentAssignmentList assignments = makeAssignments(context, declaration);
            foreach (ArgumentAssignment assignment in assignments)
            {
                IExpression expression = assignment.resolve(local, declaration, true);
				IArgument arg = assignment.getArgument ();
                IValue value = arg.checkValue(context, expression);
				if (value != null && arg.isMutable () && !value.IsMutable ())
					throw new NotMutableError ();
				local.setValue(assignment.GetName(), value);
            }
            return declaration.interpret(local);
        }

		public bool interpretAssert(Context context, TestMethodDeclaration testMethodDeclaration) {
			IValue value = this.interpret(context);
			if(value is prompto.value.Boolean)
				return ((prompto.value.Boolean)value).Value;
			else {
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
            }
            catch (PromptoError)
            {
            }
            MethodFinder finder = new MethodFinder(context, this);
            return finder.findMethod(true);
        }


    }
}