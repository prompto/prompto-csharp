using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DateTime = System.DateTime;
using prompto.error;
using prompto.grammar;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class Date : BaseValue, IComparable<Date>
    {

        public static Date Parse(String text)
        {
            System.DateTime value = System.DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            return new Date(value);
        }

        System.DateTime value;

        private Date(System.DateTime value)
			: base(DateType.Instance)
        {
            this.value = value;

        }
        public Date(int year, int month, int day)
			: base(DateType.Instance)
        {
            value = new System.DateTime(year, month, day);
        }

        public System.DateTime Value { get { return value; } }

        override
        public IValue Add(Context context, IValue value)
        {
            if (value is Period)
                return this.plus((Period)value);
            else
                throw new SyntaxError("Illegal: Date + " + value.GetType().Name);
        }

        override
        public IValue Subtract(Context context, IValue value)
        {
            if (value is Date)
            {
                System.DateTime ldt = this.Value.ToLocalTime();
                System.DateTime rdt = ((Date)value).Value.ToLocalTime();
                TimeSpan res = ldt.Subtract(rdt);
                return new Period(0, 0, 0, res.Days, res.Hours, res.Minutes, res.Seconds, res.Milliseconds);
            }
            else if (value is Period)
                return this.minus((Period)value);
            else
                throw new SyntaxError("Illegal: Date - " + value.GetType().Name);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Date)
                return this.value.CompareTo(((Date)value).value);
            else
                throw new SyntaxError("Illegal comparison: Date - " + value.GetType().Name);

        }

        override
		public IValue GetMember(Context context, String name, bool autoCreate)
        {
            if ("year" == name)
                return new Integer(this.Year);
            else if ("month" == name)
                return new Integer(this.MonthOfYear);
            else if ("dayOfMonth" == name)
                return new Integer(this.DayOfMonth);
            else if ("dayOfYear" == name)
                return new Integer(this.DayOfYear);
            else
                return base.GetMember(context, name, autoCreate);
        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }
        
        public Date minus(Period period)
        {
            return new Date(this.value.AddYears(-period.Years).AddMonths(-period.Months).AddDays(-period.Weeks * 7).AddDays(-period.Days));
        }

        public Date toDateMidnight()
        {
            return this;
        }

        public long getMillis()
        {
            return value.Ticks / 10000;
        }

        internal Date plus(Period period)
        {
            return new Date(this.value.AddYears(period.Years).AddMonths(period.Months).AddDays(period.Weeks * 7).AddDays(period.Days));
        }

        public long Year { get { return value.Year; } }

        public long MonthOfYear { get { return value.Month; } }

        public long DayOfMonth { get { return value.Day; } }

        public long DayOfYear { get { return value.DayOfYear; } }

        public int CompareTo(Date other)
        {
            return value.CompareTo(other.value);
        }

        internal bool isAfter(Date other)
        {
            return CompareTo(other) > 0;
        }

        internal Date plusDays(long days)
        {
            System.DateTime value = this.value.AddDays(days);
            return new Date(value);
        }

        override
        public bool Equals(object obj)
        {
            if (obj is Date)
                return value.Equals(((Date)obj).value);
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
            return value.ToString("yyyy-MM-dd");
        }
    }
}
