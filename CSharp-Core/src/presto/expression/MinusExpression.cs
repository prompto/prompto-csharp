using System;
using presto.runtime;
using presto.error;
using presto.value;
using Decimal = presto.value.Decimal;
using presto.parser;
using presto.type;
using presto.utils;

namespace presto.expression
{

    public class MinusExpression : IUnaryExpression
    {

        IExpression expression;

        public MinusExpression(IExpression expression)
        {
            this.expression = expression;
        }

		public override string ToString ()
		{
			return "-" + expression.ToString();
		}

        public void ToDialect(CodeWriter writer)
        {
			writer.append("-");
			expression.ToDialect(writer);
        }
  
        public IType check(Context context)
        {
            IType type = expression.check(context);
            if (type is IntegerType || type is DecimalType || type is PeriodType)
                return type;
            else
                throw new SyntaxError("Cannot negate " + type.getName());
        }

        public IValue interpret(Context context)
        {
			IValue val = expression.interpret(context);
            if (val is Integer)
                return new Integer(-((Integer)val).IntegerValue);
            else if (val is Decimal)
                return new Decimal(-((Decimal)val).DecimalValue);
            else if (val is Period)
            {
                Period p = (Period)val;
                return new Period(-p.Years, -p.Months, -p.Weeks, -p.Days, -p.Hours,
                        -p.Minutes, -p.Seconds, -p.Millis);
            }
            else
                throw new SyntaxError("Illegal: - " + val.GetType().Name);
        }

        public IExpression getExpression()
        {
            return expression;
        }

    }

}
