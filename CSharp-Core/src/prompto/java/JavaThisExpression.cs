using prompto.runtime;
using System;
using prompto.utils;
using prompto.expression;


namespace prompto.java
{

	public class JavaThisExpression : JavaExpression
	{

		ThisExpression expression;

		public JavaThisExpression()
		{
			this.expression = new ThisExpression();
		}

		public void ToDialect(CodeWriter writer) {
			expression.ToDialect(writer);
		}

	}
}