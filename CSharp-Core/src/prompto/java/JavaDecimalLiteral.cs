using System;
using prompto.runtime;
using DecimalValue = prompto.value.DecimalValue;

namespace prompto.java
{

    public class JavaDecimalLiteral : JavaLiteral
    {

        DecimalValue value;

        public JavaDecimalLiteral(String text)
			: base(text)
        {
            this.value = DecimalValue.Parse(text);
        }

        override
        public String ToString()
        {
            return value.ToString();
        }
    }
}