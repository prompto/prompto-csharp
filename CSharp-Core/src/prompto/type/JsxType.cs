using System;
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

	public override Type ToCSharpType()
	{
		throw new NotSupportedException("Should never get there!");
	}

}
}
