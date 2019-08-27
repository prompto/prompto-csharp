using System;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.jsx
{

	public class JsxSelfClosing : JsxElementBase
	{

		String nameSuite;
		String openingSuite;

		public JsxSelfClosing(String name, String nameSuite, List<JsxProperty> attributes, String openingSuite)
		: base(name, attributes)
		{
	        this.nameSuite = nameSuite;
			this.openingSuite = openingSuite;
		}

		public override void ToDialect(CodeWriter writer)
		{
			writer.append("<").append(name);
			if(nameSuite!=null)
				writer.appendRaw(nameSuite);
			else if(properties.Count>0)
				writer.append(" ");
			foreach(JsxProperty attribute in properties)
				attribute.ToDialect(writer);
			writer.append("/>");
			if(openingSuite!=null)
				writer.appendRaw(openingSuite);
		}

	}
}