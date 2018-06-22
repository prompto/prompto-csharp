using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public class JsxLiteral : IJsxValue
	{

		String text;

		public JsxLiteral(String text)
		{
			this.text = text;
		}

		public IType check(Context context)
		{
			return TextType.Instance;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(text);
		}

	}
}