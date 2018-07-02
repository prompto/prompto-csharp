using prompto.expression;
using prompto.utils;

namespace prompto.css
{

	public class CssCode : ICssValue
	{

		IExpression expression;

		public CssCode(IExpression expression)
		{
			this.expression = expression;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append("{");
			this.expression.ToDialect(writer);
			writer.append("}");
		}

	}
}
