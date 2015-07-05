using System;
using prompto.utils;

namespace prompto.python
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