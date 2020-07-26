using System;
using prompto.runtime;
using prompto.store;

namespace prompto.type
{

	public class CssType : NativeType
	{

		static CssType instance_ = new CssType();

		public static CssType Instance
		{
			get
			{
				return instance_;
			}
		}

	private CssType()
        : base(TypeFamily.CSS)
	{
	}

	public override Type ToCSharpType(Context context)
	{
		throw new NotSupportedException("Should never get there!");
	}

}
}
