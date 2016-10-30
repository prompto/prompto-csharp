using System;
using prompto.store;

namespace prompto.type
{

	public class UUIDType : NativeType
	{

		static UUIDType instance_ = new UUIDType();

		public static UUIDType Instance
		{
			get
			{
				return instance_;
			}
		}

		private UUIDType()
			: base(TypeFamily.UUID)
		{
		}

		public override Type ToCSharpType()
		{
			return typeof(Guid);
		}

	}
}
