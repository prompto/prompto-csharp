using System;
using System.Collections.Generic;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;
using System.Linq;

namespace prompto.css {

	public class CssExpression : BaseExpression, IExpression {

		List<CssField> fields;

		public CssExpression()
			: this(new List<CssField>())
        {
        }

		public CssExpression(List<CssField> fields)
        {
			this.fields = fields;
        }


        public override string ToString()
        {
            return "{ " + String.Join(", ", fields.Select(field => field.ToString())) + " }";
        }

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

		public CssExpression Plus(CssExpression expression)
		{
			HashSet<String> replacing = new HashSet<String>(expression.fields.Select(field => field.name).Distinct());
			List<CssField> result = fields.FindAll(field => !replacing.Contains(field.name));
			result.AddRange(expression.fields);
			return new CssExpression(result);;
		}

	}
}
