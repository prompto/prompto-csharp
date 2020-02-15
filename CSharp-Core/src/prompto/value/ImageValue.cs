using System;
using prompto.type;

namespace prompto.value
{
	public class ImageValue : BinaryValue
	{
		public ImageValue ()
			: base(ImageType.Instance)
		{
		}
	}
}

