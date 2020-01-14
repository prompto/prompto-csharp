using prompto.runtime;
using prompto.type;
using prompto.value;
using prompto.error;
using prompto.utils;
using prompto.parser;

namespace prompto.expression
{

	public class SuperExpression : ThisExpression, IExpression
	{

		public override IType check(Context context)
		{
			return getSuperType(context);
		}


		private IType getSuperType(Context context)
		{
            if (context != null && !(context is InstanceContext))
				context = context.getParentContext ();
			if (context is InstanceContext)
			{
				IType type = ((InstanceContext)context).getInstanceType();
				if (type is CategoryType)
					return ((CategoryType)type).getSuperType(context);
				else
					return type;
			} 
			else
				throw new SyntaxError("Not in an instance context!");
		}

		public override void ToDialect (CodeWriter writer)
		{
			writer.append ("super");
		}
	}
}