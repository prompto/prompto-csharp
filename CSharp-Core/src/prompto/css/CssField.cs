using System;
using prompto.utils;

namespace prompto.css
{

	public class CssField
	{

		String name;
		ICssValue value;

		public CssField(String name, ICssValue value)
		{
			this.name = name;
			this.value = value;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(name).append(":");
			value.ToDialect(writer);
			writer.append(";");
		}


	}
}
