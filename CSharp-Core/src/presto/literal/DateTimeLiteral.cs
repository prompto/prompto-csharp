using presto.runtime;
using System;
using presto.value;
using DateTime = presto.value.DateTime;
using presto.type;

namespace presto.literal
{

public class DateTimeLiteral : Literal<DateTime> {

	public DateTimeLiteral(String text) 
    	: base(text,parseDateTime(text.Substring(1,text.Length-2)))
	{
	}
	
	override
	public IType check(Context context) {
		return DateTimeType.Instance;
	}

    public static DateTime parseDateTime(String text)
    {
        return DateTime.Parse(text);
	}
	
}
	
	
}
