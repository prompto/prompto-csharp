using System;
using prompto.utils;


namespace prompto.python
{

    public class PythonItemExpression : PythonSelectorExpression
    {

        PythonExpression item;

        public PythonItemExpression(PythonExpression item)
        {
            this.item = item;
        }

        public override String ToString()
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
