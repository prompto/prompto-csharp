using System;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
	public abstract class DictKey
	{
		public TextValue asText()
		{
			return new TextValue(this.asKey());
		}

		internal abstract string asKey();

		public void ToDialect(CodeWriter writer)
		{
			writer.append(this.ToString());
		}
	}
}
