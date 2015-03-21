using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.utils;
using presto.value;


namespace presto.expression
{

    public class ParenthesisExpression : IExpression
    {

        IExpression expression;

        public ParenthesisExpression(IExpression expression)
        {
            this.expression = expression;
        }

		public override string ToString ()
		{
			return "(" + expression.ToString() + ")";
		}

        public void ToDialect(CodeWriter writer)
        {
			writer.append("(");
			expression.ToDialect(writer);
			writer.append(")");
        }

        public IType check(Context context)
        {
            return expression.check(context);
        }

		public IValue interpret(Context context)
        {
            return expression.interpret(context);
        }
    }
}