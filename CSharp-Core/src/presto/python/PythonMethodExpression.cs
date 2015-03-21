using System;
using presto.utils;


namespace presto.python
{

    public class PythonMethodExpression : PythonSelectorExpression
    {

        String name;
		PythonArgumentList arguments = new PythonArgumentList();

        public PythonMethodExpression(String name)
        {
            this.name = name;
        }

        public override String ToString()
        {
            return parent.ToString() + "." + name + "(" + arguments.ToString() + ")";
        }

        public void setArguments(PythonArgumentList args)
        {
            this.arguments = args;
        }

		override
		public void ToDialect(CodeWriter writer) {
			if(parent!=null) {
				parent.ToDialect(writer);
				writer.append('.');
			}
			writer.append(name);
			writer.append('(');
			if(arguments!=null)
				arguments.ToDialect(writer);
			writer.append(')');
		}
    }
}
