using System;
using prompto.runtime;
using prompto.utils;

namespace prompto.jsx {

	public class JsxAttribute
	{

		String name;
		IJsxValue value;


		public JsxAttribute(String name, IJsxValue value)
		{
			this.name = name;
			this.value = value;
		}


		public void check(Context context)
		{
			if (value != null)
				value.check(context);
		}


		public void ToDialect(CodeWriter writer)
		{
			writer.append(" ").append(name);
			if (value != null)
			{
				writer.append("=");
				value.ToDialect(writer);
			}
		}

	}

}
