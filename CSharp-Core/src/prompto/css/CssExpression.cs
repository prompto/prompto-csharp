using System.Collections.Generic;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.css {

	public class CssExpression : BaseExpression, IExpression {

		List<CssField> fields = new List<CssField>();
		
		public override IType check(Context context) {
			return CssType.Instance;
		}

		public override IValue interpret(Context context) {
			return new CssValue(this);
		}

		public override void ToDialect(CodeWriter writer) {
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
