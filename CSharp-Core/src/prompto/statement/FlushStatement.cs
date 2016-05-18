using prompto.runtime;
using prompto.type;
using prompto.value;
using prompto.store;
using prompto.utils;
using prompto.parser;

namespace prompto.statement
{
	public class FlushStatement : SimpleStatement {

		public override IType check(Context context) {
			return VoidType.Instance;
		}

		public override IValue interpret(Context context) {
			Store.Instance.flush();
			return null;
		}

		public override void ToDialect(CodeWriter writer) {
			writer.append("flush");
			if(writer.getDialect()!=Dialect.E) {
				writer.append("()");
			}
		}

	}
}
