using System;
using System.Collections.Generic;
using prompto.utils;
using System.Linq;

namespace prompto.css
{

	public class CssField
	{

		internal String name;
		List<ICssValue> values;

		public CssField(String name, List<ICssValue> values)
		{
			this.name = name;
			this.values = values;
		}

        public override string ToString()
        {
            return name + ": " + valuesToString();
        }

        public void ToDialect(CodeWriter writer)
		{
			writer.append(name).append(":");
			foreach(ICssValue value in values)
				value.ToDialect(writer);
			writer.append(";");
		}

		private String valuesToString()
        {
			return String.Join("", values.Select(value => value.ToString()));
		}

	}
}
