using prompto.runtime;
using prompto.type;
using prompto.value;
using prompto.error;
using prompto.utils;
using prompto.parser;

namespace prompto.expression
{

	public class ThisExpression : BaseExpression, IExpression
	{

		public override IType check (Context context)
		{
			if (context is DocumentContext)
				return DocumentType.Instance;
			if (context != null && !(context is InstanceContext))
				context = context.getParentContext ();
			if (context is InstanceContext)
				return ((InstanceContext)context).getInstanceType ();
			else
				throw new SyntaxError ("Not in an instance context!");
		}

		public override IValue interpret (Context context)
		{
			if (context is DocumentContext)
				return ((DocumentContext)context).getDocument();
			if (context != null && !(context is InstanceContext))
				context = context.getParentContext ();
			if (context is InstanceContext)
				return ((InstanceContext)context).getInstance ();
			else
				throw new SyntaxError ("Not in an instance context!");
		}

		public override void ToDialect (CodeWriter writer)
		{
			if (writer.getDialect () == Dialect.O)
				writer.append ("this");
			else
				writer.append ("self");

		}
	}
}