using prompto.runtime;
using System;
using prompto.utils;
using prompto.expression;


namespace prompto.javascript
{

	public class JavaScriptThisExpression : JavaScriptExpression
	{

		public void ToDialect(CodeWriter writer) {
			writer.append("this");
		}	

	}
}