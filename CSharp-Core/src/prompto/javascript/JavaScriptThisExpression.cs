using prompto.runtime;
using System;
using prompto.utils;
using prompto.expression;


namespace prompto.javascript
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