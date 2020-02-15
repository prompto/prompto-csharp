using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class DateTimeValue : BaseValue, IComparable<DateTimeValue>
    {
        public static DateTimeValue Parse(String text)
        {
            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, milli = 0;
            TimeSpan tz = new TimeSpan();
            year = Int32.Parse(text.Substring(0, 4));
            month = Int32.Parse(text.Substring(5, 2));
            day = Int32.Parse(text.Substring(8, 2));
            if (text.Length > 11)
                hour = Int32.Parse(text.Substring(11, 2));
            if (text.Length > 14)
                minute = Int32.Parse(text.Substring(14, 2));
            if (text.Length > 17)
                second = Int32.Parse(text.Substring(17, 2));
            int tzIdx = 19;
            if (text.Length > 19 && text[19] == '.')
            {
                milli = Int32.Parse(text.Substring(20, 3));
                tzIdx = 23;
            }
            if (text.Length > tzIdx)
            {
                if (text[tzIdx] == '+' || text[tzIdx] == '-')
                {
                    int sign = text[tzIdx] == '-' ? -1 : 1;
                    int offsetHours = Int32.Parse(text.Substring(tzIdx + 1, 2)) * sign;
                    int offsetMinutes = Int32.Parse(text.Substring(tzIdx + 4, 2)) * sign;
                    tz = new TimeSpan(offsetHours, offsetMinutes, 0);
                }
            }
            DateTimeOffset value = new DateTimeOffset(year, month, day, hour, minute, second, milli, tz);
            return new DateTimeValue(value);
        }

        DateTimeOffset value;

        public DateTimeValue(DateTimeOffset value)
            : base(DateTimeType.Instance)
        {
            this.value = value;
        }

        public DateTimeValue(int year, int month, int day, int hour, int minute, int second)
            : this(year, month, day, hour, minute, second, 0, new TimeSpan())
        {
        }

        public DateTimeValue(int year, int month, int day, int hour, int minute, int second, int milli)
            : this(year, month, day, hour, minute, second, milli, new TimeSpan())
        {
        }

        public DateTimeValue(int year, int month, int day, int hour, int minute, int second, int milli, TimeSpan offset)
            : base(DateTimeType.Instance)
        {
            this.value = new DateTimeOffset(year, month, day, hour, minute, second, milli, offset);
        }

        public DateTimeOffset Value { get { return value; } }


        public override IValue Add(Context context, IValue value)
        {
            if (value is PeriodValue)
                return this.plus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: DateTime + " + value.GetType().Name);
        }


        public override IValue Subtract(Context context, IValue value)
        {
            if (value is DateTimeValue)
            {
                System.DateTimeOffset ldt = this.Value.ToLocalTime();
                System.DateTimeOffset rdt = ((DateTimeValue)value).Value.ToLocalTime();
                TimeSpan res = ldt.Subtract(rdt);
                return new PeriodValue(0, 0, 0, res.Days, res.Hours, res.Minutes, res.Seconds, res.Milliseconds);
            }
            if (value is PeriodValue)
                return this.minus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: DateTime - " + value.GetType().Name);
        }


        public override Int32 CompareTo(Context context, IValue value)
        {
            if (value is DateTimeValue)
                return this.value.CompareTo(((DateTimeValue)value).value);
            else
                throw new SyntaxError("Illegal comparison: DateTime + " + value.GetType().Name);

        }


        public override IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("year" == name)
                return new IntegerValue(this.Year);
            else if ("month" == name)
                return new IntegerValue(this.MonthOfYear);
            else if ("dayOfMonth" == name)
                return new IntegerValue(this.DayOfMonth);
            else if ("dayOfYear" == name)
                return new IntegerValue(this.DayOfYear);
            else if ("hour" == name)
                return new IntegerValue(this.HourOfDay);
            else if ("minute" == name)
                return new IntegerValue(this.MinuteOfHour);
            else if ("second" == name)
                return new IntegerValue(this.SecondOfMinute);
            else if ("millisecond" == name)
                return new IntegerValue(this.MillisOfSecond);
            else if ("tzOffset" == name)
                return new IntegerValue(this.TZOffset);
            else if ("tzName" == name)
                return new TextValue(this.TZName);
            else if ("date" == name)
                return new DateValue(this.Date);
            else if ("time" == name)
                return new TimeValue(this.Time);
            else
                return base.GetMemberValue(context, name, autoCreate);
        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }

        public DateTimeValue minus(PeriodValue period)
        {
            return new DateTimeValue(this.value.AddYears(-period.Years).AddMonths(-period.Months).AddDays(-period.Weeks * 7).AddDays(-period.Days)
               .AddHours(-period.Hours).AddMinutes(-period.Minutes).AddSeconds(-period.Seconds).AddMilliseconds(-period.Millis));
        }

        public long getMillis()
        {
            return value.Ticks / 10000;
        }

        internal DateTimeValue plus(PeriodValue period)
        {
            return new DateTimeValue(this.value.AddYears(period.Years).AddMonths(period.Months).AddDays(period.Weeks * 7).AddDays(period.Days)
                .AddHours(period.Hours).AddMinutes(period.Minutes).AddSeconds(period.Seconds).AddMilliseconds(period.Millis));
        }

        public long Year { get { return value.Year; } }

        public long MonthOfYear { get { return value.Month; } }

        public long DayOfMonth { get { return value.Day; } }

        public long DayOfYear { get { return value.DayOfYear; } }

        public long HourOfDay { get { return value.Hour; } }

        public long MinuteOfHour { get { return value.Minute; } }

        public long SecondOfMinute { get { return value.Second; } }

        public long MillisOfSecond { get { return value.Millisecond; } }

        public TimeZoneInfo TimeZone { get { return FindTimeZoneByOffset(); } }

        public long TZOffset { get { return (long)TimeZone.BaseUtcOffset.TotalSeconds; } }

        public string TZName { get { return TimeZone.StandardName; } }

        public System.DateTime Date { get { return value.Date; } }

        public TimeSpan Time { get { return value.TimeOfDay; } }

        public int CompareTo(DateTimeValue other)
        {
            return this.value.CompareTo(other.value);
        }

        override
        public bool Equals(object obj)
        {
            if (obj is DateTimeValue)
                return value.Equals(((DateTimeValue)obj).value);
            else
                return value.Equals(obj);
        }

        override
        public int GetHashCode()
        {
            return value.GetHashCode();
        }

        override
        public string ToString()
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz").Replace("+00:00", "Z");
        }

        private TimeZoneInfo FindTimeZoneByOffset()
        {
            int hours = value.Offset.Hours;
            int minutes = value.Offset.Minutes;
            if (hours == 0 && minutes == 0)
                return TimeZoneInfo.Utc;
            foreach (TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
            {
                if (tzi.BaseUtcOffset.Hours == hours && tzi.BaseUtcOffset.Minutes == minutes)
                    return tzi;
            }
            return null;
        }

    }
}
