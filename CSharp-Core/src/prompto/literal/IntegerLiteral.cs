using System;
using prompto.runtime;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

    public class IntegerLiteral : Literal<IntegerValue>
    {
        public IntegerLiteral(String text)
            : base(text, IntegerValue.Parse(text))
        {
        }

        public IntegerLiteral(Int64 value)
            : base(value.ToString(), new IntegerValue(value))
        {
        }

		public IntegerLiteral(String text, IntegerValue value)
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