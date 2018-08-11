using System;

namespace prompto.literal
{
	public class DictIdentifierKey : DictKey
	{
		String id;

		public DictIdentifierKey(String id)
		{
			this.id = id;
		}

		internal override string asKey()
		{
			return this.id;
		}

		public override string ToString()
		{
			return this.id;
		}
	}
}
