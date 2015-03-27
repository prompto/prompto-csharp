using System;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.expression;
using presto.grammar;
using presto.declaration;
using presto.type;
using presto.utils;
using presto.value;


namespace presto.statement
{

	public class MethodCall : SimpleStatement, IAssertion
    {

        MethodSelector method;
        ArgumentAssignmentList assignments;

        public MethodCall(MethodSelector method)
        {
            this.method = method;
        }

        public MethodCall(MethodSelector method, ArgumentAssignmentList assignments)
        {
            this.method = method;
            this.assignments = assignments;
        }

		public override string ToString ()
		{
			return method.ToString() + (assignments != null ? assignments.ToString() : "");
		}

        public MethodSelector getMethod()
        {
            return method;
        }

        public ArgumentAssignmentList getAssignments()
        {
            return assignments;
        }

		override
		public void ToDialect(CodeWriter writer) {
			if (requiresInvoke(writer))
				writer.append("invoke: ");
			method.ToDialect(writer);
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
			Context local = method.newLocalCheckContext(context);
			return check(declaration, context, local);
        }


		private IType check(IMethodDeclaration declaration, Context parent, Context local)
        {
            if (declaration is ConcreteMethodDeclaration
				&& ((ConcreteMethodDeclaration)declaration).mustBeBeCheckedInCallContext(parent))
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
                    local.setValue(assignment.getName(), value);
                }
                return declaration.check(local);
            }
            catch (PrestoError e)
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
            Context local = method.newLocalContext(context);
            declaration.registerArguments(local);
            ArgumentAssignmentList assignments = makeAssignments(context, declaration);
            foreach (ArgumentAssignment assignment in assignments)
            {
                IExpression expression = assignment.resolve(local, declaration, true);
                IValue value = assignment.getArgument().checkValue(context, expression);
                local.setValue(assignment.getName(), value);
            }
            return declaration.interpret(local);
        }

		public bool interpretAssert(Context context, TestMethodDeclaration testMethodDeclaration) {
			IValue value = this.interpret(context);
			if(value is presto.value.Boolean)
				return ((presto.value.Boolean)value).Value;
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
                Object o = context.getValue(method.getName());
				if (o is ClosureValue)
					return new ClosureDeclaration((ClosureValue)o);
            }
            catch (PrestoError)
            {
            }
            MethodFinder finder = new MethodFinder(context, this);
            return finder.findMethod(true);
        }


    }
}