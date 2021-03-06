using prompto.utils;

namespace prompto.python
{

    public class PythonParenthesisExpression : PythonExpression
    {

        PythonExpression expression;

        public PythonParenthesisExpression(PythonExpression expression)
        {
            this.expression = expression;
        }

		public void ToDialect(CodeWriter writer) {
			writer.append('(');
			expression.ToDialect(writer);
			writer.append('(');
		}


    }
}
