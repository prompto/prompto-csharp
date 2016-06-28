using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class Time : BaseValue, IComparable<Time>
    {
        public static Time Parse(String text)
        {
            int hours = Int32.Parse(text.Substring(0, 2));
            int minutes = Int32.Parse(text.Substring(3, 2));
            int seconds = text.Length >= 8 ? Int32.Parse(text.Substring(6, 2)) : 0;
            int sep = text.IndexOf('.');
            int millis = (sep < 0) ? 0 : Int32.Parse(text.Substring(sep + 1));
            return new Time(hours, minutes, seconds, millis);
        }

        TimeSpan value;

        public Time(TimeSpan value)
			: base(TimeType.Instance)
        {
            this.value = value;
        }

        public Time(int hours, int minutes, int seconds, int millis)
			: base(TimeType.Instance)
        {
            this.value = new TimeSpan(0, hours, minutes, seconds, millis);
        }

        public TimeSpan Value { get { return value; } }

        override
        public IValue Add(Context context, IValue value)
        {
            if (value is Period)
                return this.plus((Period)value);
            else
                throw new SyntaxError("Illegal: Time + " + value.GetType().Name);
        }

        override
        public IValue Subtract(Context context, IValue value)
        {
            if (value is Time)
            {
                TimeSpan lts = this.Value;
                TimeSpan rts = ((Time)value).Value;
                TimeSpan res = lts.Subtract(rts);
                return new Period(0, 0, 0, 0, res.Hours, res.Minutes, res.Seconds, res.Milliseconds);
            }
            else if (value is Period)
                return this.minus((Period)value);
            else
                throw new SyntaxError("Illegal: Time - " + value.GetType().Name);
        }


        override
         public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Time)
                return this.value.CompareTo(((Time)value).value);
            else
                throw new SyntaxError("Illegal comparison: Time + " + value.GetType().Name);
        }

        override
		public IValue GetMember(Context context, String name, bool autoCreate)
        {
            if ("hour" == name)
                return new Integer(this.HourOfDay);
            else if ("minute" == name)
                return new Integer(this.MinuteOfHour);
            else if ("second" == name)
                return new Integer(this.SecondOfMinute);
            else if ("millisecond" == name)
                return new Integer(this.MillisOfSecond);
            else
                throw new NotSupportedException("No such member:" + name);
        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }
 
        public Time minus(Period period)
        {
            TimeSpan p = new TimeSpan(0, period.Hours, period.Minutes, period.Seconds, period.Millis);
            return new Time(value.Subtract(p));
        }

        public long getMillisOfDay()
        {
            return value.Ticks / 10000;
        }

        internal Time plus(Period period)
        {
            TimeSpan p = new TimeSpan(0, period.Hours, period.Minutes, period.Seconds, period.Millis);
            return new Time(value.Add(p));
        }

        public long HourOfDay { get { return value.Hours; } }

        public long MinuteOfHour { get { return value.Minutes; } }

        public long SecondOfMinute { get { return value.Seconds; } }

        public long MillisOfSecond { get { return value.Milliseconds; } }

        public int CompareTo(Time other)
        {
            return value.CompareTo(other.value);
        }

        internal Time plusSeconds(long seconds)
        {
            TimeSpan value = this.value;
            value = value.Add(new TimeSpan(0, 0, (int)seconds));
            return new Time(value);
        }

        internal bool isAfter(Time other)
        {
            return value.CompareTo(other.value) > 0;
        }

        override
        public bool Equals(object obj)
        {
            if (obj is Time)
                return value.Equals(((Time)obj).value);
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


    }
}
