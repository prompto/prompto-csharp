using System;
using prompto.runtime;
using prompto.utils;

namespace prompto.jsx {

	public class JsxAttribute
	{

		String name;
		IJsxValue value;
		String suite;

		public JsxAttribute(String name, IJsxValue value, String suite)
		{
			this.name = name;
			this.value = value;
        	this.suite = (suite != null && suite.Length>0) ? suite : null;
		}


		public void check(Context context)
		{
			if (value != null)
				value.check(context);
		}


		public void ToDialect(CodeWriter writer)
		{
			writer.append(name);
			if (value != null)
			{
				writer.append("=");
				value.ToDialect(writer);
			}
			if(suite!=null)
				writer.appendRaw(suite);
			else
				writer.append(" ");
		}

	}

}
