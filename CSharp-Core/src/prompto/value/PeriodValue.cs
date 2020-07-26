using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.grammar;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class PeriodValue : BaseValue, IMultiplyable
    {
        static PeriodValue zero = new PeriodValue(0, 0, 0, 0, 0, 0, 0, 0);

        public static PeriodValue ZERO { get { return zero; } }

        public static PeriodValue Parse(String text)
        {
            try
            {
                int[] data = new int[8];
                string steps = "YMWDHM.S";
                Int32? value = null;
                int lastStep = -1;
                bool isNeg = false;
                bool inPeriod = false;
                bool inTime = false;
                bool inMillis = false;

                foreach (char c in text.ToCharArray())
                {
                    // leading 'P' is mandatory
                    if (!inPeriod)
                    {
                        if (c == 'P')
                        {
                            inPeriod = true;
                            continue;
                        }
                        else
                            throw new Exception();

                    }
                    // check for time section
                    if (c == 'T')
                    {
                        if (!inTime)
                        {
                            inTime = true;
                            continue;
                        }
                        else
                            throw new Exception();
                    }
                    // check for value type
                    int step = inTime ? steps.IndexOf(c, 4) : steps.IndexOf(c);
                    if (step >= 0)
                    {
                        if (step <= lastStep)
                            throw new Exception();
                        if (step > 3 && !inTime)
                            throw new Exception();
                        if (value == null)
                            throw new Exception();
                        if (step == 6) // millis '.'
                            inMillis = true;
                        if (step == 7 && !inMillis)
                            step = 6;
                        data[step] = value.Value;
                        lastStep = step;
                        value = null;
                        continue;
                    }
                    if (c == '-')
                    {
                        if (value.HasValue)
                            throw new Exception();
                        if (isNeg || inMillis)
                            throw new Exception();
                        isNeg = true;
                    }
                    if (c < '0' || c > '9')
                        throw new Exception();
                    if (value.HasValue)
                    {
                        value *= 10;
                        value += c - '0';
                    }
                    else
                    {
                        value = c - '0';
                        if (isNeg)
                        {
                            value = -value;
                            isNeg = false;
                        }
                    }
                }
                // must terminate by a value type
                if (value != null)
                    throw new Exception();
                return new PeriodValue(data);
            }
            catch (Exception)
            {
                throw new InvalidDataError("\"" + text + "\" is not a valid ISO 8601 period!");
            }
        }

        public PeriodValue(int years, int months, int weeks, int days, int hours, int minutes, int seconds, int millis)
            : base(PeriodType.Instance)
        {
            this.Years = years;
            this.Months = months;
            this.Weeks = weeks;
            this.Days = days;
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Millis = millis;
        }

        private PeriodValue(int[] data)
            : this(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7])
        {
        }

        public int Years { get; set; }

        public int Months { get; set; }

        public int Days { get; set; }

        public int Weeks { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public int Millis { get; set; }

        
        public override IValue Add(Context context, IValue value)
        {
            if (value is PeriodValue)
                return this.plus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: Period + " + value.GetType().Name);
        }

        
        public override IValue Subtract(Context context, IValue value)
        {
            if (value is PeriodValue)
                return this.minus((PeriodValue)value);
            else
                throw new SyntaxError("Illegal: Period - " + value.GetType().Name);
        }

        
        public override IValue Multiply(Context context, IValue value)
        {
            if (value is IntegerValue)
            {
                int count = (int)((IntegerValue)value).LongValue;
                if (count < 0)
                    throw new SyntaxError("Negative repeat count:" + count);
                if (count == 0)
                    return PeriodValue.ZERO;
                if (count == 1)
                    return this;
                return this.times(count);
            }
            else
                throw new SyntaxError("Illegal: Period * " + value.GetType().Name);
        }

        
        public override Object ConvertTo(Type type)
        {
            if(type == typeof(DateTime))
                return new DateTime()
                    .AddYears(this.Years)
                    .AddMonths(this.Months)
                    .AddDays(this.Days)
                    .AddHours(this.Hours)
                    .AddMinutes(this.Minutes)
                    .AddSeconds(this.Seconds)
                    .AddMilliseconds(this.Millis);
            else
                return this;
        }

        public long TotalMilliseconds()
        {
            DateTime period = (DateTime)ConvertTo(typeof(DateTime));
            return period.Ticks / 10000; // 1 tick = 100 nanosecond
        }


        public PeriodValue minus(PeriodValue period)
        {
            double seconds = (double)(this.Seconds - period.Seconds) + (((double)(this.Millis - period.Millis)) / 1000.0);
            double millis_d = Math.Round(seconds * 1000, 0);
            int millis = Math.Abs((int)millis_d % 1000);
            return new PeriodValue(
                    this.Years - period.Years,
                    this.Months - period.Months,
                    this.Weeks - period.Weeks,
                    this.Days - period.Days,
                    this.Hours - period.Hours,
                    this.Minutes - period.Minutes,
                  (int)seconds,
                   millis);
        }



        public PeriodValue plus(PeriodValue period)
        {
            double seconds = (double)(this.Seconds + period.Seconds) + (((double)(this.Millis + period.Millis)) / 1000.0);
            double millis_d = Math.Round(seconds * 1000, 0);
            int millis = Math.Abs((int)millis_d % 1000);
            return new PeriodValue(
                   this.Years + period.Years,
                   this.Months + period.Months,
                   this.Weeks + period.Weeks,
                   this.Days + period.Days,
                   this.Hours + period.Hours,
                   this.Minutes + period.Minutes,
                   (int)seconds,
                   millis);
        }

        public PeriodValue times(int count)
        {
            double seconds = (double)(this.Seconds + ((double)(this.Millis) / 1000.0)) * count;
            double millis_d = Math.Round(seconds * 1000, 0);
            int millis = Math.Abs((int)millis_d % 1000);
            return new PeriodValue(
                  this.Years * count,
                  this.Months * count,
                  this.Weeks * count,
                  this.Days * count,
                  this.Hours * count,
                  this.Minutes * count,
                (int)seconds,
                millis);
        }

        
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder("P");
            if (Years != 0)
            {
                sb.Append(Years.ToString());
                sb.Append("Y");
            }
            if (Months != 0)
            {
                sb.Append(Months.ToString());
                sb.Append("M");
            }
            if (Weeks != 0)
            {
                sb.Append(Weeks.ToString());
                sb.Append("W");
            }
            if (Days != 0)
            {
                sb.Append(Days.ToString());
                sb.Append("D");
            }
            if (Hours != 0 || Minutes != 0 || Seconds != 0 || Millis != 0)
                sb.Append("T");
            if (Hours != 0)
            {
                sb.Append(Hours.ToString());
                sb.Append("H");
            }
            if (Minutes != 0)
            {
                sb.Append(Minutes.ToString());
                sb.Append("M");
            }
            if (Seconds != 0 || Millis != 0)
            {
                sb.Append(Seconds.ToString());
                if (Millis != 0)
                {
                    sb.Append(".");
                    sb.Append(Millis.ToString("000"));
                }
                sb.Append("S");
            }
            return sb.ToString();
        }

        
        public override bool Equals(object obj)
        {
            if (obj is PeriodValue)
            {
                PeriodValue period = (PeriodValue)obj;
                return this.Years == period.Years
                     && this.Months == period.Months
                     && this.Weeks == period.Weeks
                     && this.Days == period.Days
                     && this.Hours == period.Hours
                     && this.Minutes == period.Minutes
                     && this.Seconds == period.Seconds
                     && this.Millis == period.Millis;
            }
            else
                return false;
        }

        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override IValue ToDocumentValue(Context context)
        {
            return new TextValue(ToString());
        }


    }
}
