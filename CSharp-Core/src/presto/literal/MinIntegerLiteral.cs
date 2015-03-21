using System;
using presto.value;

namespace presto.literal
{

	public class MinIntegerLiteral : IntegerLiteral {

		public MinIntegerLiteral()
			: base("MIN_INTEGER", new Integer(Int64.MinValue))
		{

		}
	}

}

