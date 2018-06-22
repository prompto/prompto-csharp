using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public class JsxExpression : IJsxValue, IJsxExpression
	{

		IExpression expression;

		public JsxExpression(IExpression expression)
		{
			this.expression = expression;
		}

		public IType check(Context context)
		{
			return expression.check(context);
		}

		public IValue interpret(Context context)
		{
			return new JsxValue(this);
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append("{");
			expression.ToDialect(writer);
			writer.append("}");
		}

	}

}