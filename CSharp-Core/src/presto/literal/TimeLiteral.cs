using presto.runtime;
using System;
using presto.value;
using presto.type;

namespace presto.literal
{

    public class TimeLiteral : Literal<Time>
    {

        public TimeLiteral(String text)
            : base(text, parseTime(text.Substring(1, text.Length - 2)))
        {
        }

        override public IType check(Context context)
        {
            return TimeType.Instance;
        }

        public static Time parseTime(String text)
        {
            return Time.Parse(text);
        }


    }
	
}
