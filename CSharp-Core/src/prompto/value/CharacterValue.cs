using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.error;
using prompto.runtime;
using System.Globalization;
using prompto.type;
using Newtonsoft.Json.Linq;

namespace prompto.value
{
    public class CharacterValue : BaseValue, IComparable<CharacterValue>, IMultiplyable
    {
        char value;

        public CharacterValue(char value)
			: base(CharacterType.Instance)
        {
            this.value = value;
        }

        public char Value { get { return value; } }

		public override object GetStorableData()
		{
			return value;
		}

		public TextValue AsText()
		{
			return new TextValue (value.ToString ());
		}

		public override IValue GetMemberValue(Context context, String name, bool autoCreate)
		{
			if ("codePoint" == name)
				return new IntegerValue((int)value);
			else
				return base.GetMemberValue(context, name, autoCreate);
		}

		
        public override IValue Add(Context context, IValue value)
        {
            return new TextValue(this.value.ToString() + value.ToString());
        }

        
        public override IValue Multiply(Context context, IValue value)
        {
            if (value is IntegerValue)
            {
                int count = (int)((IntegerValue)value).LongValue;
                if (count < 0)
                    throw new SyntaxError("Negative repeat count:" + count);
                if (count == 0)
                    return new TextValue("");
                if (count == 1)
                    return new TextValue(this.value.ToString());
                char[] cc = new char[count];
                for (int i = 0; i < count; i++)
                    cc[i] = this.value;
                return new TextValue(new String(cc));
          }
            else
               throw new SyntaxError("Illegal: Chararacter * " + value.GetType().Name);
         }

        public int CompareTo(CharacterValue obj)
        {
            return value.CompareTo(obj.Value);
        }

        
        public override Int32 CompareTo(Context context, IValue value)
        {
            if (value is CharacterValue)
                return this.value.CompareTo(((CharacterValue)value).value);
            else
                throw new SyntaxError("Illegal comparison: Character + " + value.GetType().Name);

        }

        
        
        public override Object ConvertTo(Context context, Type type)
        {
            return value;
        }
        
        
        public override String ToString()
        {
            return Value.ToString();
        }

        
        public override bool Equals(object obj)
        {
            if (obj is CharacterValue)
                return value.Equals(((CharacterValue)obj).value);
            else
                return value.Equals(obj);
        }

		
		public override bool Roughly(Context context, IValue obj)
		{
			if (obj is CharacterValue || obj is TextValue) 
			{
				return string.Compare(value.ToString(), obj.ToString(), true)==0;
			}
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
