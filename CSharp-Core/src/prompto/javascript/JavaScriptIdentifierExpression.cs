using System;
using prompto.utils;

namespace prompto.javascript
{

	public class JavaScriptIdentifierExpression : JavaScriptExpression
	{

		public static JavaScriptIdentifierExpression parse (String ids)
		{
			String[] parts = ids.Split ("\\.".ToCharArray());
			JavaScriptIdentifierExpression result = null;
			foreach (String part in parts)
				result = new JavaScriptIdentifierExpression (result, part);
			return result;
		}

		JavaScriptIdentifierExpression parent;
		String identifier;

		public JavaScriptIdentifierExpression (String identifier)
		{
			this.identifier = identifier;
		}

		public JavaScriptIdentifierExpression (JavaScriptIdentifierExpression parent, String identifier)
		{
			this.parent = parent;
			this.identifier = identifier;
		}

		public JavaScriptIdentifierExpression getParent ()
		{
			return parent;
		}

		public String getIdentifier ()
		{
			return identifier;
		}

		override
		public String ToString ()
		{
			if (parent == null)
				return identifier;
			else
				return parent.ToString () + "." + identifier;
		}

		public void ToDialect (CodeWriter writer)
		{
			if (parent != null) {
				parent.ToDialect (writer);
				writer.append ('.');
			}
			writer.append (identifier);
		}

	}
}