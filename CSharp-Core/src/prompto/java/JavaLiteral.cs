using System;
using prompto.runtime;
using prompto.utils;
using prompto.type;


namespace prompto.java
{

    public abstract class JavaLiteral : JavaExpression
    {
		String text;

		protected JavaLiteral(String text) {
			this.text = text;
		}

		
		public override String ToString()
		{
			return text;
		}

		public override void ToDialect(CodeWriter writer) {
			writer.append(text);
		}

    }
}
