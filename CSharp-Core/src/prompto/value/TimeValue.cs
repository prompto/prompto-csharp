using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using prompto.error;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json.Linq;

namespace prompto.value
{
    public class TimeValue : BaseValue, IComparable<TimeValue>
    {
        public static TimeValue Parse(String text)
        {
            int hours = Int32.Parse(text.Substring(0, 2));
            int minutes = Int32.Parse(text.Substring(3, 2));
            int seconds = text.Length >= 8 ? Int32.Parse(text.Substring(6, 2)) : 0;
            int sep = text.IndexOf('.');
            int millis = (sep < 0) ? 0 : Int32.Parse(text.Substring(sep + 1));
            return new TimeValue(hours, minutes, seconds, millis);
        }

        TimeSpan value;

        public TimeValue(TimeSpan value)
            : base(TimeType.Instance)
        {
            this.value = value;
        }

        public TimeValue(int hours, int minutes, int seconds, int millis)
            : base(TimeType.Instance)
        {
            this.value = new TimeSpan(0, hours, minutes, seconds, millis);
        }

        public TimeSpan Value { get { return value; } }

        
        public override IValue Add(Context context, IValue value)
        {
            if (value is PeriodValue)
                return this.plus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: Time + " + value.GetType().Name);
        }

        
        public override IValue Subtract(Context context, IValue value)
        {
            if (value is TimeValue)
            {
                TimeSpan lts = this.Value;
                TimeSpan rts = ((TimeValue)value).Value;
                TimeSpan res = lts.Subtract(rts);
                return new PeriodValue(0, 0, 0, 0, res.Hours, res.Minutes, res.Seconds, res.Milliseconds);
            }
            else if (value is PeriodValue)
                return this.minus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: Time - " + value.GetType().Name);
        }


        
        public override Int32 CompareTo(Context context, IValue value)
        {
            if (value is TimeValue)
                return this.value.CompareTo(((TimeValue)value).value);
            else
                throw new SyntaxError("Illegal comparison: Time + " + value.GetType().Name);
        }

        
        public override IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("hour" == name)
                return new IntegerValue(this.HourOfDay);
            else if ("minute" == name)
                return new IntegerValue(this.MinuteOfHour);
            else if ("second" == name)
                return new IntegerValue(this.SecondOfMinute);
            else if ("millisecond" == name)
                return new IntegerValue(this.MillisOfSecond);
            else
                return base.GetMemberValue(context, name, autoCreate);
        }

        
        public override Object ConvertTo(Context context, Type type)
        {
            return value;
        }

        public TimeValue minus(PeriodValue period)
        {
            TimeSpan p = new TimeSpan(0, period.Hours, period.Minutes, period.Seconds, period.Millis);
            return new TimeValue(value.Subtract(p));
        }

        public long getMillisOfDay()
        {
            return value.Ticks / 10000;
        }

        internal TimeValue plus(PeriodValue period)
        {
            TimeSpan p = new TimeSpan(0, period.Hours, period.Minutes, period.Seconds, period.Millis);
            return new TimeValue(value.Add(p));
        }

        public long HourOfDay { get { return value.Hours; } }

        public long MinuteOfHour { get { return value.Minutes; } }

        public long SecondOfMinute { get { return value.Seconds; } }

        public long MillisOfSecond { get { return value.Milliseconds; } }

        public int CompareTo(TimeValue other)
        {
            return value.CompareTo(other.value);
        }

        internal TimeValue plusSeconds(long seconds)
        {
            TimeSpan value = this.value;
            value = value.Add(new TimeSpan(0, 0, (int)seconds));
            return new TimeValue(value);
        }

        internal bool isAfter(TimeValue other)
        {
            return value.CompareTo(other.value) > 0;
        }

        
        public override bool Equals(object obj)
        {
            if (obj is TimeValue)
                return value.Equals(((TimeValue)obj).value);
            else
                return value.Equals(obj);
        }

        
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value.Hours.ToString("00"));
            sb.Append(':');
            sb.Append(value.Minutes.ToString("00"));
            sb.Append(':');
            sb.Append(value.Seconds.ToString("00"));
            sb.Append('.');
            sb.Append(value.Milliseconds.ToString("000"));
            return sb.ToString();
        }

        public override IValue ToDocumentValue(Context context)
        {
            return new TextValue(ToString());
        }

        public override JToken ToJsonToken()
        {
            return new JValue(this.ToString());
        }

    }
}
