using System;
using presto.utils;

namespace presto.python
{

    public abstract class PythonLiteral : PythonExpression
    {
		String text;

		protected PythonLiteral(String text) {
			this.text = text;
		}

		public void ToDialect(CodeWriter writer) {
			writer.append(text);
		}

    }
}