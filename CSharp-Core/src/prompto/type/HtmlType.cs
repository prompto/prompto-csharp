using System;
using prompto.store;

namespace prompto.type
{

	public class HtmlType : NativeType
	{

		static HtmlType instance_ = new HtmlType();

		public static HtmlType Instance
		{
			get
			{
				return instance_;
			}
		}

		private HtmlType()
			: base(TypeFamily.HTML)
		{
		}

		public override bool isAssignableFrom(runtime.Context context, IType other)
		{
			if(other==JsxType.Instance)
				return true;
			else
				return base.isAssignableFrom(context, other);
		}

		public override Type ToCSharpType()
		{
			throw new NotSupportedException("Should never get there!");
		}

	}
}
