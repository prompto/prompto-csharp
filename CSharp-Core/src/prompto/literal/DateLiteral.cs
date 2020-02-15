using prompto.value;
using System;
using prompto.runtime;
using prompto.type;
namespace prompto.literal
{

    public class DateLiteral : Literal<DateValue>
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

        public static DateValue parseDate(String text)
        {
            return DateValue.Parse(text);
        }

    }
	
	
}
