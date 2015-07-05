using prompto.value;
using System;
using prompto.runtime;
using prompto.type;
namespace prompto.literal
{

    public class DateLiteral : Literal<Date>
    {

        public DateLiteral(String text)
            : base(text, parseDate(text.Substring(1, text.Length - 2)))
        {
        }

        override
        public IType check(Context context)
        {
            return DateType.Instance;
        }

        public static Date parseDate(String text)
        {
            return Date.Parse(text);
        }

    }
	
	
}
