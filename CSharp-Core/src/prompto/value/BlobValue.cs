using System;
using prompto.type;

namespace prompto.value
{
	public class BlobValue : BinaryValue
	{
		public BlobValue ()
			: base(BlobType.Instance)
		{
		}

		public BlobValue (String mimeType, byte[] data)
			: base(BlobType.Instance, mimeType, data)
		{
		}
	}
}

