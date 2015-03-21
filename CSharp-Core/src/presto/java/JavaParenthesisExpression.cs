using presto.runtime;
using System;
using presto.utils;


namespace presto.java
{

    public class JavaParenthesisExpression : JavaExpression
    {

        JavaExpression expression;

        public JavaParenthesisExpression(JavaExpression expression)
        {
            this.expression = expression;
        }

		public void ToDialect(CodeWriter writer) {
			writer.append('(');
			expression.ToDialect(writer);
			writer.append(')');
		}
		    
	}
}