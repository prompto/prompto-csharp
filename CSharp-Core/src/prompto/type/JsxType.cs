using System;
using prompto.runtime;
using prompto.store;

namespace prompto.type
{

	public class JsxType : NativeType
	{

		static JsxType instance_ = new JsxType();

		public static JsxType Instance
		{
			get
			{
				return instance_;
			}
		}

	private JsxType()
        : base(TypeFamily.JSX)
	{
	}

	public override Type ToCSharpType(Context context)
	{
		throw new NotSupportedException("Should never get there!");
	}

}
}
