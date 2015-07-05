using System;
using prompto.utils;


namespace prompto.python
{

	public class PythonOrdinalArgument : PythonArgument
    {

        PythonExpression expression;

		public PythonOrdinalArgument(PythonExpression expression)
        {
            this.expression = expression;
        }

		public void ToDialect(CodeWriter writer) {
			expression.ToDialect(writer);
		}
    }
}
