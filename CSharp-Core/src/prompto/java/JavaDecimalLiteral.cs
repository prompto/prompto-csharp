using System;
using prompto.runtime;
using Decimal = prompto.value.Decimal;

namespace prompto.java
{

    public class JavaDecimalLiteral : JavaLiteral
    {

        Decimal value;

        public JavaDecimalLiteral(String text)
			: base(text)
        {
            this.value = Decimal.Parse(text);
        }

        override
        public String ToString()
        {
            return value.ToString();
        }
    }
}