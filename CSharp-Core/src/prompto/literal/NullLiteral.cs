using prompto.expression;
using prompto.type;
using prompto.runtime;
using prompto.value;
using prompto.utils;
using prompto.parser;

namespace prompto.literal
{

	public class NullLiteral : IExpression
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

		public IType check (Context context)
		{
			return NullType.Instance;
		}

		public IValue interpret (Context context)
		{
			return NullValue.Instance;
		}

		public void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				writer.append ("nothing");
				break;
			case Dialect.O:
				writer.append ("null");
				break;
			case Dialect.S:
				writer.append ("None");
				break;
			}
		}

	}
}