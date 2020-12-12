using System;
using prompto.utils;

namespace prompto.css
{

	public class CssField
	{

		internal String name;
		ICssValue value;

		public CssField(String name, ICssValue value)
		{
			this.name = name;
			this.value = value;
		}

        public override string ToString()
        {
            return name + ": " + value.ToString();
        }

        public void ToDialect(CodeWriter writer)
		{
			writer.append(name).append(":");
			value.ToDialect(writer);
			writer.append(";");
		}


	}
}
