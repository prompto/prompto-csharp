using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class Boolean : BaseValue
    {
        public static readonly Boolean TRUE = new Boolean(true);
        public static readonly Boolean FALSE = new Boolean(false);

        static Boolean()
        {
            TRUE.not = FALSE;
            FALSE.not = TRUE;
        }

        public static Boolean Parse(string text)
        {
            return ValueOf( bool.Parse(text));
        }

        public static Boolean ValueOf(bool value)
        {
            return value ? TRUE : FALSE;
        }
        
        bool value;
        Boolean not;

        private Boolean(bool value)
			: base(BooleanType.Instance)
        {
            this.value = value;
        }

        public bool Value { get { return value; } }

        public Boolean Not { get { return not; } }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Boolean)
                return this.value.CompareTo(((Boolean)value).value);
            else
                throw new SyntaxError("Illegal comparison: Boolean + " + value.GetType().Name);
 
        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }
        
        override
        public String ToString()
        {
            return Value.ToString().ToLower();
        }

        override
        public bool Equals(object obj)
        {
            if (obj is Boolean)
                return value.Equals(((Boolean)obj).value);
            else
                return value.Equals(obj);
        }

        override
        public int GetHashCode()
        {
            return value.GetHashCode();
        }


    }
}