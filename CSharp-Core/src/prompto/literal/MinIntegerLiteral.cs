using System;
using prompto.value;

namespace prompto.literal
{

	public class MinIntegerLiteral : IntegerLiteral {

		public MinIntegerLiteral()
			: base("MIN_INTEGER", new Integer(Int64.MinValue))
		{

		}
	}

}

