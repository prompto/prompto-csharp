using System;
using presto.value;

namespace presto.literal
{
	public class MaxIntegerLiteral : IntegerLiteral {

		public MaxIntegerLiteral()
			: base("MAX_INTEGER", new Integer(Int64.MaxValue))
		{

		}
	}
}

