using System;
using prompto.type;

namespace prompto.value
{
 
	public class BinaryValue : BaseValue
	{
		protected BinaryValue (IType type)
			: base(type)
		{
		}

		protected BinaryValue (IType type, String mimeType, byte[] data)
			: base(type)
		{
			MimeType = mimeType;
			Data = data;
		}

		public String MimeType { get; set; }

		public byte[] Data { get; set; }
	}
}

