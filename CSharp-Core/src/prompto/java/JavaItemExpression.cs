using prompto.error;
using prompto.grammar;
using prompto.runtime;
using System;
using prompto.type;
using prompto.utils;

namespace prompto.java
{

    public class JavaItemExpression : JavaSelectorExpression
    {

        JavaExpression item;

        public JavaItemExpression(JavaExpression item)
        {
            this.item = item;
        }

        override
        public String ToString()
        {
            return parent.ToString() + "[" + item.ToString() + "]";
        }

		override
		public void ToDialect(CodeWriter writer) {
			parent.ToDialect(writer);
			writer.append('[');
			item.ToDialect(writer);
			writer.append(']');
		}

    }

}