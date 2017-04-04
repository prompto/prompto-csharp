using prompto.runtime;
using System;
using prompto.utils;
using prompto.expression;


namespace prompto.java
{

	public class JavaThisExpression : JavaExpression
	{

		public void ToDialect(CodeWriter writer) {
			writer.append("this");
		}

	}
}