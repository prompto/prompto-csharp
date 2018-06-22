using System;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.jsx
{

	public class JsxSelfClosing : JsxElementBase
	{

		public JsxSelfClosing(String name, List<JsxAttribute> attributes)
		: base(name, attributes)
		{
		}

		public override void ToDialect(CodeWriter writer)
		{
			writer.append("<").append(name);
			foreach(JsxAttribute attribute in attributes)
				attribute.ToDialect(writer);
			writer.append("/>");
		}

	}
}