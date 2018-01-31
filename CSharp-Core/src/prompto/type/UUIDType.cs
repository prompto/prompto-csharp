using System;
using prompto.runtime;
using prompto.store;
using prompto.value;

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

		public override IValue ConvertCSharpValueToIValue(Context context, object value)
		{
			if (value is Guid)
				return new UUIDValue((Guid)value);
			else
				return base.ConvertCSharpValueToIValue(context, value);
		}

	}
}
