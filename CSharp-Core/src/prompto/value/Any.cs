using System;
using prompto.type;

namespace prompto.value
{
	public class Any : BaseValue
	{
		public Any()
			: base(AnyType.Instance)
		{
		}

		public Object Id {
			get { return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this); }
			set { /* nothing to do, this is just required for exposing Any as a Presto category */ }
		}

		public String Text { get; set; }

		public override String ToString() {
			return "{id:" + System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this) + ", text:" + Text + "}";
		}
	}
}

