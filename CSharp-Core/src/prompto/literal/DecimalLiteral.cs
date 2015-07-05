using System;
using prompto.runtime;
using Decimal = prompto.value.Decimal;
using prompto.type;

namespace prompto.literal
{

    public class DecimalLiteral : Literal<Decimal>
    {

        public DecimalLiteral(String text)
            : base(text, Decimal.Parse(text))
        {
        }

        override
        public IType check(Context context)
        {
            return DecimalType.Instance;
        }
    }

}
