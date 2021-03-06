using System;
using prompto.utils;

namespace prompto.javascript
{

	public class JavaScriptMemberExpression : JavaScriptSelectorExpression
	{

		String name;

		public JavaScriptMemberExpression (String name)
		{
			this.name = name;
		}


		public override String ToString ()
		{
			return parent.ToString () + "." + name;
		}


		public override void ToDialect (CodeWriter writer)
		{
			parent.ToDialect (writer);
			writer.append ('.');
			writer.append (name);
		}

	}

}
