using System;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.jsx
{

	public class JsxElement : JsxElementBase
	{

		List<IJsxExpression> children;

		public JsxElement(String name, List<JsxAttribute> attributes)
		: base(name, attributes)
		{
		}

		public void setChildren(List<IJsxExpression> children)
		{
			this.children = children;
		}

		public override void ToDialect(CodeWriter writer)
		{
			writer.append("<").append(name);
			foreach(JsxAttribute attr in attributes) {
				attr.ToDialect(writer);
			}
			writer.append(">");
			if (children != null)
				foreach (IJsxExpression child in children)
				{
					child.ToDialect(writer);
				}
			writer.append("</").append(name).append(">");
		}

	}
}