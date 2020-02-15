using prompto.value;
using System;
using prompto.runtime;
using prompto.type;
namespace prompto.literal
{

    public class PeriodLiteral : Literal<PeriodValue>
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

        public static PeriodValue parsePeriod(String text)
        {
            return PeriodValue.Parse(text);
        }

    }
	
	
}
