using System;
using presto.runtime;
using Decimal = presto.value.Decimal;

namespace presto.java
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