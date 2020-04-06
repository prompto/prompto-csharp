using System;
using prompto.intrinsic;
using prompto.type;

namespace prompto.value
{
	public class BlobValue : BinaryValue
	{
		public BlobValue ()
			: base(BlobType.Instance)
		{
		}

		public BlobValue(Binary binary)
			: base(BlobType.Instance, binary)
		{
		}

		public BlobValue (String mimeType, byte[] data)
			: base(BlobType.Instance, mimeType, data)
		{
		}
	}
}

