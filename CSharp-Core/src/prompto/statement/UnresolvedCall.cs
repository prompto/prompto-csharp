using prompto.expression;
using System;
using prompto.type;
using prompto.runtime;
using prompto.declaration;
using prompto.error;
using prompto.utils;
using prompto.value;
using prompto.grammar;
using prompto.parser;


namespace prompto.statement
{


	public class UnresolvedCall : BaseStatement, IAssertion
	{

		protected IExpression resolved;
		protected IExpression caller;
		protected ArgumentList arguments;

		public UnresolvedCall(UnresolvedCall call)
			: this(call.caller, call.arguments)
		{
		}

		public UnresolvedCall(IExpression caller, ArgumentList arguments)
		{
			this.caller = caller;
			this.arguments = arguments;
		}

		public override bool IsSimple
		{
			get
			{
				return true;
			}
		}


		public IExpression getCaller()
		{
			return caller;
		}


		public void setCaller(IExpression caller)
		{
			this.caller = caller;
		}

		public ArgumentList getArguments()
		{
			return arguments;
		}


		public override void ToDialect(CodeWriter writer)
		{
			try
			{
				resolve(writer.getContext());
				if(resolved!=null)
                    resolved.ToDialect(writer);
			}
			catch (SyntaxError /*e*/)
			{
				caller.ToDialect(writer);
				if (arguments != null)
					arguments.ToDialect(writer);
				else if (writer.getDialect() != Dialect.E)
					writer.append("()");
			}
		}


		public override IType check(Context context)
		{
			return resolveAndCheck(context);
		}


		public override IValue interpret(Context context)
		{
			resolve(context);
			return resolved.interpret(context); ;
		}

		public bool interpretAssert(Context context, TestMethodDeclaration testMethodDeclaration)
		{
			if (resolved == null)
				resolveAndCheck(context);
			if (resolved is IAssertion)
				return ((IAssertion)resolved).interpretAssert(context, testMethodDeclaration);
			else
			{
				CodeWriter writer = new CodeWriter(this.Dialect, context);
				resolved.ToDialect(writer);
				throw new SyntaxError("Cannot test '" + writer.ToString() + "'");
			}
		}

		protected IType resolveAndCheck(Context context)
		{
			resolve(context);
			return resolved.check(context);
		}


		private void resolve(Context context)
		{
			if (resolved != null)
				return;
			if (caller is UnresolvedIdentifier)
				resolved = resolveUnresolvedIdentifier(context, (UnresolvedIdentifier)caller);
			else if (caller is UnresolvedSelector)
				resolved = resolveUnresolvedSelector(context, (UnresolvedSelector)caller);
			else if (caller is MemberSelector)
				resolved = resolveMemberSelector(context, (MemberSelector)caller);
			if (resolved == null)
				throw new SyntaxError("Unknown method: " + this.ToString());
		}

		private IExpression resolveUnresolvedSelector(Context context, UnresolvedSelector caller)
		{
            caller.resolveMethod(context, arguments);
			return caller.getResolved();
		}

		private IExpression resolveUnresolvedIdentifier(Context context, UnresolvedIdentifier caller)
		{
			String name = caller.getName();
			// if this happens in the context of a member method, then we need to check for category members first
			IExpression call = resolveUnresolvedMemberMethod(context, name);
			if (call == null)
				call = resolveUnresolvedMethodReference(context, name);
			if (call == null)
				call = resolveUnresolvedDeclaration(context, name);
			if (call == null)
				throw new SyntaxError("Unknown name:" + name);
			return call;
		}

        private IExpression resolveUnresolvedDeclaration(Context context, string name)
        {
			IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(name);
			if (decl == null)
				return null;
			else if (decl is CategoryDeclaration)
					return new ConstructorExpression(new CategoryType(name), null, arguments);
			else
				return new MethodCall(new MethodSelector(name), arguments);
	    }

        private IExpression resolveUnresolvedMethodReference(Context context, string name)
        {
			INamed named = context.getRegisteredValue<INamed>(name);
			if (named == null)
				return null;
			IType type = named.GetIType(context).Resolve(context);
			if (type is MethodType)
			{
				MethodCall call = new MethodCall(new MethodSelector(name), arguments);
				call.setVariableName(name);
				return call;
			}
			else
				return null;
		}

        private IExpression resolveUnresolvedMemberMethod(Context context, string name)
        {
			while(context != null)
            {
				InstanceContext instance = context.getClosestInstanceContext();
				if (instance == null)
					return null;

				IDeclaration decl = resolveUnresolvedMember(instance, name);
				if (decl != null)
					return new MethodCall(new MethodSelector(name), arguments);
				else
					context = instance.getParentContext();
			}
			return null;
	    }

        private IDeclaration resolveUnresolvedMember(InstanceContext context, String name)
		{
			ConcreteCategoryDeclaration decl = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(context.getInstanceType().GetTypeName());
			MethodDeclarationMap methods = decl.getMemberMethods(context, name);
			if (methods != null && methods.Count > 0)
				return methods;
			else
				return null;
		}

		private IExpression resolveMemberSelector(Context context, MemberSelector caller)
		{
			IExpression parent = caller.getParent();
			String name = caller.getName();
			return new MethodCall(new MethodSelector(parent, name), arguments);
		}

		public void setParent(IExpression parent)
		{
			if(parent!=null) {
				if(caller is UnresolvedIdentifier)
					caller = new MethodSelector(parent, ((UnresolvedIdentifier)caller).getName());
				else if(caller is SelectorExpression)
					((SelectorExpression)caller).setParent(parent);
				else
					throw new InvalidOperationException("Should never happen!");
			}
		}
	}
}