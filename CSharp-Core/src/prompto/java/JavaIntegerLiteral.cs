using prompto.error;
using prompto.grammar;
using prompto.runtime;
using System;
using prompto.value;

namespace prompto.java
{

    public class JavaIntegerLiteral : JavaLiteral
    {

        Integer value;

        public JavaIntegerLiteral(String text)
			: base(text)
        {
            this.value = Integer.Parse(text);
        }

 
        override
        public String ToString()
        {
            return value.ToString();
        }
    }

}