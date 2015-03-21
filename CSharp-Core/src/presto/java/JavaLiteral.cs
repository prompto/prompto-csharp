using System;
using presto.runtime;
using presto.utils;
using presto.type;


namespace presto.java
{

    public abstract class JavaLiteral : JavaExpression
    {
		String text;

		protected JavaLiteral(String text) {
			this.text = text;
		}

		override
		public String ToString()
		{
			return text;
		}

		public void ToDialect(CodeWriter writer) {
			writer.append(text);
		}

    }
}
