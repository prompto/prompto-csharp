using presto.error;
using presto.grammar;
using presto.runtime;
using System;
using presto.value;

namespace presto.java
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