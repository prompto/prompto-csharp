using presto.runtime;
using System;
using presto.utils;
using presto.expression;


namespace presto.java
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