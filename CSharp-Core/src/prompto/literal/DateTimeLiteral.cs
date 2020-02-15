using prompto.runtime;
using System;
using prompto.value;
using DateTimeValue = prompto.value.DateTimeValue;
using prompto.type;

namespace prompto.literal
{

public class DateTimeLiteral : Literal<DateTimeValue> {

	public DateTimeLiteral(String text) 
    	: base(text,parseDateTime(text.Substring(1,text.Length-2)))
	{
	}
	
	override
	public IType check(Context context) {
		return DateTimeType.Instance;
	}

    public static DateTimeValue parseDateTime(String text)
    {
        return DateTimeValue.Parse(text);
	}
	
}
	
	
}
