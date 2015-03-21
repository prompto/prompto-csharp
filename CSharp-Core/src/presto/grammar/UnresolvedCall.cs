using presto.statement;
using presto.expression;
using System;
using presto.parser;
using presto.type;
using presto.runtime;
using presto.declaration;
using presto.error;
using presto.utils;
using presto.value;


namespace presto.grammar
{


	public class UnresolvedCall : SimpleStatement
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
                resolved = new ConstructorExpression(new CategoryType(name), assignments);
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