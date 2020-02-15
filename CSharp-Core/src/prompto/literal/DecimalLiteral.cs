using System;
using prompto.runtime;
using DecimalValue = prompto.value.DecimalValue;
using prompto.type;

namespace prompto.literal
{

    public class DecimalLiteral : Literal<DecimalValue>
    {

        public DecimalLiteral(String text)
            : base(text, DecimalValue.Parse(text))
        {
        }

        override
        public IType check(Context context)
        {
            return DecimalType.Instance;
        }
    }

}
