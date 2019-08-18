using prompto.expression;
using prompto.type;
using prompto.runtime;
using prompto.value;
using prompto.utils;
using prompto.parser;

namespace prompto.literal
{

	public class NullLiteral : BaseExpression, IExpression
	{

		static NullLiteral instance_ = new NullLiteral ();

		public static NullLiteral Instance {
			get {
				return instance_;
			}
		}

		private NullLiteral ()
		{
		}

		public override IType check (Context context)
		{
			return NullType.Instance;
		}

		public override IValue interpret (Context context)
		{
			return NullValue.Instance;
		}

		public override void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				writer.append ("nothing");
				break;
			case Dialect.O:
				writer.append ("null");
				break;
			case Dialect.M:
				writer.append ("None");
				break;
			}
		}

	}
}