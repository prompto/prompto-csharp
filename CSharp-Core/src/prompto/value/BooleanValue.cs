using System;
using Newtonsoft.Json.Linq;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class BooleanValue : BaseValue
    {
        public static readonly BooleanValue TRUE = new BooleanValue(true);
        public static readonly BooleanValue FALSE = new BooleanValue(false);

        static BooleanValue()
        {
            TRUE.not = FALSE;
            FALSE.not = TRUE;
        }

        public static BooleanValue Parse(string text)
        {
            return ValueOf( bool.Parse(text));
        }

        public static BooleanValue ValueOf(bool value)
        {
            return value ? TRUE : FALSE;
        }
        
        bool value;
        BooleanValue not;

        private BooleanValue(bool value)
			: base(BooleanType.Instance)
        {
            this.value = value;
        }

        public bool Value { get { return value; } }

        public BooleanValue Not { get { return not; } }

		public override object GetStorableData()
		{
			return value;
		}

       public override Int32 CompareTo(Context context, IValue value)
        {
            if (value is BooleanValue)
                return this.value.CompareTo(((BooleanValue)value).value);
            else
                throw new SyntaxError("Illegal comparison: Boolean + " + value.GetType().Name);
 
        }

        
        public override Object ConvertTo(Type type)
        {
            return value;
        }
        
        
        public override String ToString()
        {
            return Value.ToString().ToLower();
        }

        
        public override bool Equals(object obj)
        {
            if (obj is BooleanValue)
                return value.Equals(((BooleanValue)obj).value);
            else
                return value.Equals(obj);
        }

        
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override JToken ToJsonToken()
        {
            return new JValue(value);
        }



    }
}
