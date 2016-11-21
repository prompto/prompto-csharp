using prompto.error;
using prompto.runtime;
using prompto.statement;
using prompto.type;

namespace prompto.expression
{
	public class UnresolvedSelector : SelectorExpression
	{

		string name;
		IExpression resolved;


		public UnresolvedSelector(string name)
		{
			this.name = name;
		}

		public IExpression getResolved()
		{
			return resolved;
		}

		public string getName()
		{
			return name;
		}


		public override string ToString()
		{
			return (parent == null ? "" : parent.ToString() + '.') + name;
		}

		public override void ToDialect(utils.CodeWriter writer)
		{
			try
			{
				resolve(writer.getContext(), false);
			}
			catch (SyntaxError )
			{
			}
			if (resolved != null)
				resolved.ToDialect(writer);
			else {
				if (parent != null)
				{
					parent.ToDialect(writer);
					writer.append('.');
				}
				writer.append(name);
			}
		}

		public override IType check(Context context)
		{
			return resolveAndCheck(context, false);
		}

		public IType checkMember(Context context)
		{
			return resolveAndCheck(context, true);
		}

		public override value.IValue interpret(Context context)
		{

			resolveAndCheck(context, false);
			return resolved.interpret(context);
		}


		private IType resolveAndCheck(Context context, bool forMember)
		{
			resolve(context, forMember);
			return resolved != null ? resolved.check(context) : AnyType.Instance;
		}

		public IExpression resolve(Context context, bool forMember)
		{
			if (resolved == null)
			{
				resolved = resolveMethod(context);
				if (resolved == null)
					resolved = resolveMember(context);
			}
			if (resolved == null)
				throw new SyntaxError("Unknown identifier:" + name);
			return resolved;
		}

		private IExpression resolveMember(Context context)
		{
			try
			{
				IExpression member = new MemberSelector(parent, name);
				member.check(context);
				return member;
			}
			catch (SyntaxError)
			{
				return null;
			}
		}

		private IExpression resolveMethod(Context context)
		{
			try
			{
				IExpression resolvedParent = parent;
				if (resolvedParent is UnresolvedIdentifier)
				{
					((UnresolvedIdentifier)resolvedParent).checkMember(context);
					resolvedParent = ((UnresolvedIdentifier)resolvedParent).getResolved();
				}
				IExpression method = new UnresolvedCall(new MethodSelector(resolvedParent, name), null);
				method.check(context);
				return method;
			}
			catch (SyntaxError)
			{
				return null;
			}
		}



	}

}
