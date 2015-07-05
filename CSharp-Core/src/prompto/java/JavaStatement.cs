using System;
using prompto.runtime;
using prompto.parser;
using prompto.utils;


namespace prompto.java
{

    public class JavaStatement
    {
        JavaExpression expression;
        bool isReturn;

        public JavaStatement(JavaExpression expression, bool isReturn)
        {
            this.expression = expression;
            this.isReturn = isReturn;
        }

        override
        public String ToString()
        {
            return "" + (isReturn ? "return " : "") + expression.ToString() + ";";
        }

		public void ToDialect(CodeWriter writer)
		{
			if (isReturn)
				writer.append ("return ");
			expression.ToDialect (writer);
			writer.append (';');
		}

    }
}
