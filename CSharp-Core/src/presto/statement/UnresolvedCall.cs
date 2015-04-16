using presto.expression;
using System;
using presto.parser;
using presto.type;
using presto.runtime;
using presto.declaration;
using presto.error;
using presto.utils;
using presto.value;
using presto.grammar;


namespace presto.statement
{


	public class UnresolvedCall : SimpleStatement, IAssertion
    {

        IExpression resolved;
        IExpression caller;
        ArgumentAssignmentList assignments;

        public UnresolvedCall(IExpression caller, ArgumentAssignmentList assignments)
        {
            this.caller = caller;
            this.assignments = assignments;
        }

        public IExpression getCaller()
        {
            return caller;
        }

        public ArgumentAssignmentList getAssignments()
        {
            return assignments;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			try {
				resolve(writer.getContext());
				resolved.ToDialect(writer);
			} catch(SyntaxError /*e*/) {
				caller.ToDialect(writer);
				if(assignments!=null)
					assignments.ToDialect(writer);
			}
        }

        override
        public IType check(Context context)
        {
            return resolveAndCheck(context);
        }

        override
		public IValue interpret(Context context)
        {
            if (resolved == null)
                resolveAndCheck(context);
            return resolved.interpret(context);
        }

		public bool interpretAssert(Context context, TestMethodDeclaration testMethodDeclaration) {
			if(resolved==null)
				resolveAndCheck(context);
			if(resolved is IAssertion)
				return ((IAssertion)resolved).interpretAssert(context, testMethodDeclaration);
			else {
				CodeWriter writer = new CodeWriter(this.Dialect, context);
				resolved.ToDialect(writer);
				throw new SyntaxError("Cannot test '" + writer.ToString() + "'");
			}
		}

		private IType resolveAndCheck(Context context) {
			resolve (context);
			return resolved.check (context);
		}


		private void resolve(Context context) {
			if(resolved!=null)
				return;
			if(caller is UnresolvedIdentifier)
				resolveUnresolvedIdentifier(context);
			else
				resolveMember(context);
		}


		private void resolveUnresolvedIdentifier(Context context)
        {
            String name = ((UnresolvedIdentifier)caller).getName();
            IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(name);
            if (decl == null)
                throw new SyntaxError("Unknown name:" + name);
            if (decl is CategoryDeclaration)
                resolved = new ConstructorExpression(new CategoryType(name), false, assignments);
            else
                resolved = new MethodCall(new MethodSelector(name), assignments);
        }

        private void resolveMember(Context context)
        {
            IExpression parent = ((MemberSelector)caller).getParent();
            String name = ((MemberSelector)caller).getName();
            resolved = new MethodCall(new MethodSelector(parent, name), assignments);
        }

    }
}