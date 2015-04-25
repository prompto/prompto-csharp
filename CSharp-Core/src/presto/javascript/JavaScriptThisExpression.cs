using presto.runtime;
using System;
using presto.utils;
using presto.expression;


namespace presto.javascript
{

	public class JavaScriptThisExpression : JavaScriptExpression
	{

		ThisExpression expression;

		public JavaScriptThisExpression()
		{
			this.expression = new ThisExpression();
		}

		public void ToDialect(CodeWriter writer) {
			expression.ToDialect(writer);
		}

	}
}