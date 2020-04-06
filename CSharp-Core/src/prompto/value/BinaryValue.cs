using System;
using prompto.intrinsic;
using prompto.type;

namespace prompto.value
{
 
	public class BinaryValue : BaseValue
	{
		Binary binary;

		protected BinaryValue (IType type)
			: base(type)
		{
		}

		protected BinaryValue(IType type, Binary binary)
			: base(type)
		{
			this.binary = binary;
		}

		protected BinaryValue (IType type, String mimeType, byte[] data)
			: base(type)
		{
			binary = new Binary(mimeType, data);
		}

		public String MimeType {
            get {
				return binary.MimeType;
			}
			set
			{
				binary.MimeType = value;
			}
		}

		public byte[] Data
		{
			get
			{
				return binary.Data;
			}
			set
			{
				binary.Data = value;
			}
		}
	}
}

