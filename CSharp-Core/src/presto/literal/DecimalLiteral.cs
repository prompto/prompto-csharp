using System;
using presto.runtime;
using Decimal = presto.value.Decimal;
using presto.type;

namespace presto.literal
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
