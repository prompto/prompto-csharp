using System;
using prompto.value;

namespace prompto.literal
{

	public class MinIntegerLiteral : IntegerLiteral {

		public MinIntegerLiteral()
			: base("MIN_INTEGER", new IntegerValue(Int64.MinValue))
		{

		}
	}

}

