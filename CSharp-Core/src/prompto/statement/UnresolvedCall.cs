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


	public class UnresolvedCall : BaseStatement, IAssertion
	{

		protected IExpression resolved;
		protected IExpression caller;
		protected ArgumentAssignmentList assignments;

		public UnresolvedCall(UnresolvedCall call)
			: this(call.caller, call.assignments)
		{ 
		}

		public UnresolvedCall(IExpression caller, ArgumentAssignmentList assignments)
		{
			this.caller = caller;
			this.assignments = assignments;
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

		public ArgumentAssignmentList getAssignments()
		{
			return assignments;
		}


		public override void ToDialect(CodeWriter writer)
		{
			try
			{
				resolve(writer.getContext());
				resolved.ToDialect(writer);
			}
			catch (SyntaxError /*e*/)
			{
				caller.ToDialect(writer);
				if (assignments != null)
					assignments.ToDialect(writer);
			}
		}


		public override IType check(Context context)
		{
			return resolveAndCheck(context);
		}


		public override IValue interpret(Context context)
		{
			resolve(context);
			return resolved.interpret(context);;
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
				resolved = resolveUnresolvedIdentifier(context);
			else if (caller is MemberSelector)
				resolved = resolveMember(context);
		}


		private IExpression resolveUnresolvedIdentifier(Context context)
		{
			String name = ((UnresolvedIdentifier)caller).getName();
			IExpression call = null;
			IDeclaration decl = null;
			// if this happens in the context of a member method, then we need to check for category members first
			InstanceContext instance = context.getClosestInstanceContext();
			if (instance != null)
			{
				decl = resolveUnresolvedMember(instance, name);
				if (decl != null)
					call = new MethodCall(new MethodSelector(name), assignments);
			}
			if (call == null)
			{
				INamed named = context.getRegisteredValue<INamed>(name);
				if (named != null)
				{
					IType type = named.GetIType(context);
					if (type is MethodType)
					{
						call = new MethodCall(new MethodSelector(name), assignments);
						((MethodCall)call).setVariableName(name);
					}
				}
			}
			if (call == null)
			{
				decl = context.getRegisteredDeclaration<IDeclaration>(name);
				if (decl == null)
					throw new SyntaxError("Unknown name:" + name);
				if (decl is CategoryDeclaration)
					return new ConstructorExpression(new CategoryType(name), null, assignments, false);
				else
					return new MethodCall(new MethodSelector(name), assignments);
			}
			return call;
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

		private IExpression resolveMember(Context context)
		{
			IExpression parent = ((MemberSelector)caller).getParent();
			String name = ((MemberSelector)caller).getName();
			return new MethodCall(new MethodSelector(parent, name), assignments);
		}

	}
}