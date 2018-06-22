using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public class JsxText : IJsxExpression
	{

		String text;


		public JsxText(String text)
		{
			this.text = text;
		}

		public IType check(Context context)
		{
			return TextType.Instance;
		}

		public IValue interpret(Context context)
		{
			return new JsxValue(this);
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(text);
		}

	}
}