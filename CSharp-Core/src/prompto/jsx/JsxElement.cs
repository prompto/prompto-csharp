using System;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.jsx
{

	public class JsxElement : JsxElementBase
	{

		String nameSuite;
		String openingSuite;
		List<IJsxExpression> children;
		JsxClosing closing;

		public JsxElement(String name, String nameSuite, List<JsxAttribute> attributes, String openingSuite)
		: base(name, attributes)
		{
            this.nameSuite = nameSuite;
			this.openingSuite = openingSuite;
		}

		public void setClosing(JsxClosing closing)
		{
			this.closing = closing;
		}

		public void setChildren(List<IJsxExpression> children)
		{
			this.children = children;
		}

		public override void ToDialect(CodeWriter writer)
		{
			writer.append("<").append(name);
			if(nameSuite!=null)
				writer.appendRaw(nameSuite);
			else if(attributes.Count>0)
				writer.append(" ");
			foreach(JsxAttribute attr in attributes) {
				attr.ToDialect(writer);
			}
			writer.append(">");
			if(openingSuite!=null)
				writer.appendRaw(openingSuite);
			if (children != null)
				foreach (IJsxExpression child in children)
				{
					child.ToDialect(writer);
				}
			closing.ToDialect(writer);
		}

	}
}