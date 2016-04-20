using System;
using prompto.type;

namespace prompto.value
{
	public class Blob : BinaryValue
	{
		public Blob ()
			: base(BlobType.Instance)
		{
		}

		public Blob (String mimeType, byte[] data)
			: base(BlobType.Instance, mimeType, data)
		{
		}
	}
}

