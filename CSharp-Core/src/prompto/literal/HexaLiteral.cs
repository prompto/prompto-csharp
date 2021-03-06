using System;
using prompto.runtime;
using prompto.value;
using prompto.type;
namespace prompto.literal
{

    public class HexaLiteral : Literal<IntegerValue>
    {


        public HexaLiteral(String text)
            : base(text, parseHexa(text))
        {
        }

        override
        public IType check(Context context)
        {
            return IntegerType.Instance;
        }

        static public IntegerValue parseHexa(String text)
        {
            long value = 0;
            foreach (char c in text.Substring(2).ToCharArray())
            {
                value <<= 4;
                if (c >= '0' && c <= '9')
                    value += (c - '0');
                else if (c >= 'a' && c <= 'f')
                    value += (c - 'a');
                else if (c >= 'A' && c <= 'F')
                    value += 10 + (c - 'A');
                else
                    throw new Exception(text + " is not a valid hexadecimal number");
            }
            return new IntegerValue(value);
        }
    }
}
