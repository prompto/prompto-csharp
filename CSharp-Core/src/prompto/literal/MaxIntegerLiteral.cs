using System;
using prompto.value;

namespace prompto.literal
{
	public class MaxIntegerLiteral : IntegerLiteral {

		public MaxIntegerLiteral()
			: base("MAX_INTEGER", new IntegerValue(Int64.MaxValue))
		{

		}
	}
}

