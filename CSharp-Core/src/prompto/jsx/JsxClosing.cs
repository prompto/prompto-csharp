using System;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.jsx
{

	public class JsxClosing
	{

		String name;
		String suite;

		public JsxClosing(String name, String suite)
		{
			this.name = name;
			this.suite = suite;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append("</").append(name).append(">");
			if(suite!=null)
				writer.appendRaw(suite);
		}

	}
}