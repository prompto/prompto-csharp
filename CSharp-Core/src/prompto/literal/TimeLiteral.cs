using prompto.runtime;
using System;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

    public class TimeLiteral : Literal<TimeValue>
    {

        public TimeLiteral(String text)
            : base(text, parseTime(text.Substring(1, text.Length - 2)))
        {
        }

        override public IType check(Context context)
        {
            return TimeType.Instance;
        }

        public static TimeValue parseTime(String text)
        {
            return TimeValue.Parse(text);
        }


    }
	
}
