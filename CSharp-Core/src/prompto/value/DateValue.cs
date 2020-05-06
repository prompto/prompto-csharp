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
    public class DateValue : BaseValue, IComparable<DateValue>
    {

        public static DateValue Parse(String text)
        {
            System.DateTime value = System.DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            return new DateValue(value);
        }

        System.DateTime value;

        public DateValue(System.DateTime value)
            : base(DateType.Instance)
        {
            this.value = value;

        }
        public DateValue(int year, int month, int day)
            : base(DateType.Instance)
        {
            value = new System.DateTime(year, month, day);
        }

        public System.DateTime Value { get { return value; } }

        public override object GetStorableData()
        {
            return value;
        }

        public override IValue Add(Context context, IValue value)
        {
            if (value is PeriodValue)
                return this.plus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: Date + " + value.GetType().Name);
        }

        
        public override IValue Subtract(Context context, IValue value)
        {
            if (value is DateValue)
            {
                System.DateTime ldt = this.Value.ToLocalTime();
                System.DateTime rdt = ((DateValue)value).Value.ToLocalTime();
                TimeSpan res = ldt.Subtract(rdt);
                return new PeriodValue(0, 0, 0, res.Days, res.Hours, res.Minutes, res.Seconds, res.Milliseconds);
            }
            else if (value is PeriodValue)
                return this.minus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: Date - " + value.GetType().Name);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is DateValue)
                return this.value.CompareTo(((DateValue)value).value);
            else
                throw new SyntaxError("Illegal comparison: Date - " + value.GetType().Name);

        }

        override
        public IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("year" == name)
                return new IntegerValue(this.Year);
            else if ("month" == name)
                return new IntegerValue(this.MonthOfYear);
            else if ("dayOfMonth" == name)
                return new IntegerValue(this.DayOfMonth);
            else if ("dayOfYear" == name)
                return new IntegerValue(this.DayOfYear);
            else
                return base.GetMemberValue(context, name, autoCreate);
        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }

        public DateValue minus(PeriodValue period)
        {
            return new DateValue(this.value.AddYears(-period.Years).AddMonths(-period.Months).AddDays(-period.Weeks * 7).AddDays(-period.Days));
        }

        public DateValue toDateMidnight()
        {
            return this;
        }

        public long getMillis()
        {
            return value.Ticks / 10000;
        }

        internal DateValue plus(PeriodValue period)
        {
            return new DateValue(this.value.AddYears(period.Years).AddMonths(period.Months).AddDays(period.Weeks * 7).AddDays(period.Days));
        }

        public long Year { get { return value.Year; } }

        public long MonthOfYear { get { return value.Month; } }

        public long DayOfMonth { get { return value.Day; } }

        public long DayOfYear { get { return value.DayOfYear; } }

        public int CompareTo(DateValue other)
        {
            return value.CompareTo(other.value);
        }

        internal bool isAfter(DateValue other)
        {
            return CompareTo(other) > 0;
        }

        internal DateValue plusDays(long days)
        {
            System.DateTime value = this.value.AddDays(days);
            return new DateValue(value);
        }

        override
        public bool Equals(object obj)
        {
            if (obj is DateValue)
                return value.Equals(((DateValue)obj).value);
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
