using prompto.error;
using prompto.grammar;
using prompto.runtime;
using System;
using prompto.value;

namespace prompto.java
{

    public class JavaIntegerLiteral : JavaLiteral
    {

        IntegerValue value;

        public JavaIntegerLiteral(String text)
			: base(text)
        {
            this.value = IntegerValue.Parse(text);
        }

 
        override
        public String ToString()
        {
            return value.ToString();
        }
    }

}