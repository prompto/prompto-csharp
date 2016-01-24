using prompto.expression;
using System;
using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.declaration;
using prompto.error;
using prompto.utils;
using prompto.value;
using prompto.grammar;


namespace prompto.statement
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
				resolved = resolveUnresolvedIdentifier(context);
			else if(caller is MemberSelector)
				resolved = resolveMember(context);
		}


		private IExpression resolveUnresolvedIdentifier(Context context)
        {
			String name = ((UnresolvedIdentifier)caller).getName();
			IDeclaration decl = null;
			// if this happens in the context of a member method, then we need to check for category members first
			if(context.getParentContext() is InstanceContext) {
				decl = resolveUnresolvedMember((InstanceContext)context.getParentContext(), name);
				if(decl!=null)
					return new MethodCall(new MethodSelector(name), assignments);
			}
			decl = context.getRegisteredDeclaration<IDeclaration>(name);
			if(decl==null)
				throw new SyntaxError("Unknown name:" + name);
			if(decl is CategoryDeclaration)
				return new ConstructorExpression(new CategoryType(name), assignments);
			else
				return new MethodCall(new MethodSelector(name), assignments);
       }

		private IDeclaration resolveUnresolvedMember(InstanceContext context, String name) {
			ConcreteCategoryDeclaration decl = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(context.getInstanceType().GetName());
			MethodDeclarationMap methods = decl.getMemberMethods(context, name);
			if(methods!=null && methods.Count>0)
				return methods;
			else
				return null;
		}

		private IExpression resolveMember(Context context)
        {
            IExpression parent = ((MemberSelector)caller).getParent();
            String name = ((MemberSelector)caller).getName();
            return new MethodCall(new MethodSelector(parent, name), assignments);
        }

    }
}