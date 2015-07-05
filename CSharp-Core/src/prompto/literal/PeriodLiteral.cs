using prompto.value;
using System;
using prompto.runtime;
using prompto.type;
namespace prompto.literal
{

    public class PeriodLiteral : Literal<Period>
    {

        public PeriodLiteral(String text)
            : base(text, parsePeriod(text.Substring(1, text.Length - 2)))
        {
        }

        override
        public IType check(Context context)
        {
            return PeriodType.Instance;
        }

        public static Period parsePeriod(String text)
        {
            return Period.Parse(text);
        }

    }
	
	
}
