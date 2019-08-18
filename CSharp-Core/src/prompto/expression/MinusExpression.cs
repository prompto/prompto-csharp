using System;
using prompto.runtime;
using prompto.error;
using prompto.value;
using Decimal = prompto.value.Decimal;
using prompto.parser;
using prompto.type;
using prompto.utils;

namespace prompto.expression
{

    public class MinusExpression : BaseExpression, IUnaryExpression
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

        public override void ToDialect(CodeWriter writer)
        {
			writer.append("-");
			expression.ToDialect(writer);
        }
  
        public override IType check(Context context)
        {
            IType type = expression.check(context);
            if (type is IntegerType || type is DecimalType || type is PeriodType)
                return type;
            else
				throw new SyntaxError("Cannot negate " + type.GetTypeName());
        }

        public override IValue interpret(Context context)
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
