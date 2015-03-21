using System;
using presto.utils;

namespace presto.javascript
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