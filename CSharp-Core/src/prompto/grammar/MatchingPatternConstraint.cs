using prompto.runtime;
using System;
using prompto.error;
using System.Text.RegularExpressions;
using prompto.expression;
using prompto.utils;
using prompto.value;


namespace prompto.grammar
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
