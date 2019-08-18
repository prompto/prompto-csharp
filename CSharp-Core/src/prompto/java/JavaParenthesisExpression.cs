using prompto.runtime;
using System;
using prompto.utils;


namespace prompto.java
{

    public class JavaParenthesisExpression : JavaExpression
    {

        JavaExpression expression;

        public JavaParenthesisExpression(JavaExpression expression)
        {
            this.expression = expression;
        }

		public override void ToDialect(CodeWriter writer) {
			writer.append('(');
			expression.ToDialect(writer);
			writer.append(')');
		}
		    
	}
}