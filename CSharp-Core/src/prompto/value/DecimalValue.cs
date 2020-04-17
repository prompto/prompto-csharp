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
    public class DecimalValue : BaseValue, INumber, IComparable<INumber>, IMultiplyable
    {
        public static DecimalValue Parse(String text)
        {
            return new DecimalValue(Double.Parse(text, CultureInfo.InvariantCulture));
        }

        Double value;

        public DecimalValue(Double value)
			: base(DecimalType.Instance)
        {
            this.value = value;
        }

        public Int64 LongValue { get { return (Int64)value; } }
        
        public Double DoubleValue { get { return value; } }

        public override object GetStorableData()
        {
            return value;
        }

        
        public override IValue Add(Context context, IValue value)
        {
            if (value is IntegerValue)
                return new DecimalValue(this.value + ((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                return new DecimalValue(this.value + ((DecimalValue)value).DoubleValue);
            else
                throw new SyntaxError("Illegal: Decimal + " + value.GetType().Name);
        }

        
        public override IValue Subtract(Context context, IValue value)
        {
            if (value is IntegerValue)
                return new DecimalValue(this.value - ((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                return new DecimalValue(this.value - ((DecimalValue)value).DoubleValue);
            else
                throw new SyntaxError("Illegal: Decimal - " + value.GetType().Name);
        }

        
        public override IValue Multiply(Context context, IValue value)
        {
             if (value is IntegerValue)
                 return new DecimalValue(this.DoubleValue * ((IntegerValue)value).LongValue);
            else if (value is DecimalValue)
                 return new DecimalValue(this.DoubleValue * ((DecimalValue)value).DoubleValue);
            else
                throw new SyntaxError("Illegal: Decimal * " + value.GetType().Name);
       }

        
        public override IValue Divide(Context context, IValue value)
        {
            if (value is INumber)
            {
                if(((INumber)value).DoubleValue == 0.0)
                    throw new DivideByZeroError();
                else
                    return new DecimalValue(this.DoubleValue / ((INumber)value).DoubleValue);
             }
            else
                throw new SyntaxError("Illegal: Decimal / " + value.GetType().Name);
        }

		public override IValue IntDivide(Context context, IValue value) {
			if (value is IntegerValue) {
				if (((IntegerValue) value).LongValue == 0)
					throw new DivideByZeroError();
				else
					return new IntegerValue(this.LongValue / ((IntegerValue) value).LongValue);
			} else
				throw new SyntaxError("Illegal: Decimal \\ " + value.GetType().Name);
		}

		public override IValue Modulo(Context context, IValue value) {
			if (value is INumber) {
				if (((INumber) value).DoubleValue == 0.0)
					throw new DivideByZeroError();
				else
					return new DecimalValue(this.DoubleValue % ((INumber) value).DoubleValue);
			} else
				throw new SyntaxError("Illegal: Decimal % " + value.GetType().Name);
		}

        public int CompareTo(INumber obj)
        {
            return value.CompareTo(obj.DoubleValue);
        }

        
        public override Int32 CompareTo(Context context, IValue value)
        {
            if (value is INumber)
                return this.value.CompareTo(((INumber)value).DoubleValue);
            else
                throw new SyntaxError("Illegal comparison: Decimal and " + value.GetType().Name);

        }

        
        public override Object ConvertTo(Type type)
        {
            return value;
        }
 
        
        public override String ToString()
        {
            return value.ToString("0.0######", CultureInfo.InvariantCulture);
        }

         
        public override bool Equals(object obj)
        {
			if (obj is DecimalValue)
				return value == ((DecimalValue)obj).value;
			else if (obj is IntegerValue)
				return value == ((IntegerValue)obj).LongValue;
			else
				return value.Equals(obj);
        }

        
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

    }
}
