using prompto.runtime;
using System;
using prompto.utils;
using prompto.expression;


namespace prompto.python
{

	public class PythonSelfExpression : PythonExpression
	{

		public void ToDialect(CodeWriter writer) {
			writer.append("self");
		}

	}
}