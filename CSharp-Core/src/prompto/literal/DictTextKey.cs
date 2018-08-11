using System;
using prompto.utils;

namespace prompto.literal
{
	public class DictTextKey : DictKey
	{
		String text;

		public DictTextKey(String text)
		{
			this.text = text;
		}

		internal override string asKey()
		{
			return StringUtils.Unescape( this.text);
		}

		public override string ToString()
		{
			return this.text;
		}
	}
}
