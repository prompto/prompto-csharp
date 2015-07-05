using System;
using prompto.runtime;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

    public class IntegerLiteral : Literal<Integer>
    {
        public IntegerLiteral(String text)
            : base(text, Integer.Parse(text))
        {
        }

        public IntegerLiteral(Int64 value)
            : base(value.ToString(), new Integer(value))
        {
        }

		public IntegerLiteral(String text, Integer value)
			: base(text, value)
		{
		}

        override
        public IType check(Context context)
        {
            return IntegerType.Instance;
        }

    }
}