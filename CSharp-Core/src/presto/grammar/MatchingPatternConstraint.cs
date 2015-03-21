using presto.runtime;
using System;
using presto.error;
using System.Text.RegularExpressions;
using presto.expression;
using presto.utils;
using presto.value;


namespace presto.grammar
{

    public class MatchingPatternConstraint : IAttributeConstraint
    {

        IExpression expression;
        Regex pattern;

        public MatchingPatternConstraint(IExpression expression)
        {
            this.expression = expression;
        }

        public void checkValue(Context context, IValue value)
        {
            if (pattern == null)
            {
				IValue toMatch = expression.interpret(context);
                pattern = new Regex(toMatch.ToString());
            }
            if (!pattern.IsMatch(value.ToString()))
                throw new InvalidDataError(value.ToString() + " does not match:" + pattern.ToString());
        }

		public void ToDialect(CodeWriter writer) 
		{
			writer.append(" matching ");
			expression.ToDialect(writer);
		}

    }

}
