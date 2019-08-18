using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;


namespace prompto.expression
{

    public class ParenthesisExpression : BaseExpression, IExpression
    {

        IExpression expression;

        public ParenthesisExpression(IExpression expression)
        {
            this.expression = expression;
        }


		public IExpression getExpression()
		{
			return expression;
		}

		public override string ToString ()
		{
			return "(" + expression.ToString() + ")";
		}

        public override void ToDialect(CodeWriter writer)
        {
			writer.append("(");
			expression.ToDialect(writer);
			writer.append(")");
        }

        public override IType check(Context context)
        {
            return expression.check(context);
        }

		public override IValue interpret(Context context)
        {
            return expression.interpret(context);
        }
    }
}