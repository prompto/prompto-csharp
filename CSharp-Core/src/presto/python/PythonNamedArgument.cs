using System;
using presto.utils;


namespace presto.python
{

	public class PythonNamedArgument : PythonArgument
    {

        String name;
        PythonExpression expression;

        public PythonNamedArgument(String name, PythonExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

		public void ToDialect(CodeWriter writer) {
			writer.append(name);
			writer.append(" = ");
			expression.ToDialect(writer);
		}
    }
}
