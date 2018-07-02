using System.Collections.Generic;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.css {

	public class CssExpression : IExpression {

		List<CssField> fields = new List<CssField>();
		
		public IType check(Context context) {
			return CssType.Instance;
		}

		public IValue interpret(Context context) {
			return new CssValue(this);
		}

		public void ToDialect(CodeWriter writer) {
			writer.append("{");
			foreach (CssField field in fields)
			{
				field.ToDialect(writer);
			}
			writer.append("}");
		}

		public void AddField(CssField field) {
			fields.Add(field);
		}
		

	}
}
