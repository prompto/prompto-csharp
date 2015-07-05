using System;
using prompto.utils;

namespace prompto.javascript
{

	public abstract class JavaScriptLiteral : JavaScriptExpression
	{

		String text;

		protected JavaScriptLiteral (String text)
		{
			this.text = text;
		}

		public void ToDialect (CodeWriter writer)
		{
			writer.append (text);
		}

	}
}