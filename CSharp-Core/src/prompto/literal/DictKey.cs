using System;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
	public abstract class DictKey
	{
		public Text asText()
		{
			return new Text(this.asKey());
		}

		internal abstract string asKey();

		public void ToDialect(CodeWriter writer)
		{
			writer.append(this.ToString());
		}
	}
}
