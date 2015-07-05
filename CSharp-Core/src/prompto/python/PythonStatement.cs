using System;
using prompto.utils;


namespace prompto.python
{

    public class PythonStatement
    {

        PythonExpression expression;
		PythonModule module;
		bool isReturn;

        public PythonStatement(PythonExpression expression, bool isReturn)
        {
            this.expression = expression;
            this.isReturn = isReturn;
        }

		public void setModule(PythonModule module) {
			this.module = module;
		}

		public override String ToString()
        {
            return "" + (isReturn ? "return " : "") + expression.ToString() + ";";
        }

		public void ToDialect(CodeWriter writer) {
			if(isReturn)
				writer.append("return ");
			expression.ToDialect(writer);
			if(module!=null)
				module.ToDialect(writer);
		}
    }
}