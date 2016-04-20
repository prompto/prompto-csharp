using System;
using prompto.type;

namespace prompto.value
{
	public class Image : BinaryValue
	{
		public Image ()
			: base(ImageType.Instance)
		{
		}
	}
}

