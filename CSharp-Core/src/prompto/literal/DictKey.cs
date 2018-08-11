using System;
using prompto.runtime;
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
}
}
