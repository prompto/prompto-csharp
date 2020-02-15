using System;
using prompto.error;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace prompto.value
{
    public class IntegerValue : BaseValue, INumber, IComparable<INumber>, IMultiplyable
    {
        public static IntegerValue Parse(String text)
        {
            return new IntegerValue(Int64.Parse(text));
        }

        long value;

        public IntegerValue(long value)
            : base(IntegerType.Instance)
        {
            this.value = value;
        }

        public Int64 LongValue { get { return value; } }

        public Double DoubleValue { get { return value; } }


        public override object GetStorableData()
        {
            return value;
        }

        public override IValue Add(Context context, IValue value)
        {
            if (value is IntegerValue)
                return new IntegerValue(this.LongValue + ((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                return new DecimalValue(((DecimalValue)value).DoubleValue + this.value);
            else
                throw new SyntaxError("Illegal: Integer + " + value.GetType().Name);
        }


        public override IValue Subtract(Context context, IValue value)
        {
            if (value is IntegerValue)
                return new IntegerValue(this.LongValue - ((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                return new DecimalValue(this.DoubleValue - ((DecimalValue)value).DoubleValue);
            else
                throw new SyntaxError("Illegal: Integer - " + value.GetType().Name);
        }

        override
        public IValue Multiply(Context context, IValue value)
        {
            if (value is IntegerValue)
                return new IntegerValue(this.LongValue * ((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                return new DecimalValue(((DecimalValue)value).DoubleValue * this.LongValue);
            else if (value is IMultiplyable)
                return value.Multiply(context, this);
            else
                throw new SyntaxError("Illegal: Integer * " + value.GetType().Name);
        }

        override
        public IValue Divide(Context context, IValue value)
        {
            if (value is INumber)
            {
                if (((INumber)value).DoubleValue == 0.0)
                    throw new DivideByZeroError();
                else
                    return new DecimalValue(this.DoubleValue / ((INumber)value).DoubleValue);
            }
            else
                throw new SyntaxError("Illegal: Integer / " + value.GetType().Name);
        }

        override
        public IValue IntDivide(Context context, IValue value)
        {
            if (value is IntegerValue)
            {
                if (((IntegerValue)value).LongValue == 0)
                    throw new DivideByZeroError();
                else
                    return new IntegerValue(this.LongValue / ((IntegerValue)value).LongValue);
            }
            else
                throw new SyntaxError("Illegal: Integer \\ " + value.GetType().Name);
        }

        override
        public IValue Modulo(Context context, IValue value)
        {
            if (value is IntegerValue)
            {
                try
                {
                    return new IntegerValue(this.LongValue % ((IntegerValue)value).LongValue);
                }
                catch (DivideByZeroException)
                {
                    throw new DivideByZeroError();
                }
            }
            else
                throw new SyntaxError("Illegal: Integer % " + value.GetType().Name);
        }

        public int CompareTo(INumber obj)
        {
            return value.CompareTo(obj.LongValue);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is IntegerValue)
                return this.value.CompareTo(((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                return this.DoubleValue.CompareTo(((DecimalValue)value).DoubleValue);
            else
                throw new SyntaxError("Illegal comparison: Integer and " + value.GetType().Name);

        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }

        override
        public String ToString()
        {
            return value.ToString();
        }


        public override bool Equals(object obj)
        {
            if (obj is DecimalValue)
                return value == ((DecimalValue)obj).DoubleValue;
            else if (obj is IntegerValue)
                return value == ((IntegerValue)obj).value;
            else
                return value.Equals(obj);
        }


        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override void ToJson(Context context, JsonWriter generator, object instanceId, String fieldName, bool withType, Dictionary<string, byte[]> binaries)
        {
            generator.WriteValue(value);
        }

    }
}
