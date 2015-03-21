using presto.error;
using presto.grammar;
using presto.runtime;
using System;
using presto.type;
using presto.utils;

namespace presto.java
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