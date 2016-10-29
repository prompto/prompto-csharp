using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.error;
using prompto.runtime;
using System.Globalization;
using prompto.type;

namespace prompto.value
{
    public class Character : BaseValue, IComparable<Character>, IMultiplyable
    {
        char value;

        public Character(char value)
			: base(CharacterType.Instance)
        {
            this.value = value;
        }

        public char Value { get { return value; } }

		public override object GetStorableData()
		{
			return value;
		}

		public Text AsText()
		{
			return new Text (value.ToString ());
		}

		public override IValue GetMember(Context context, String name, bool autoCreate)
		{
			if ("codePoint" == name)
				return new Integer((int)value);
			else
				throw new NotSupportedException("No such member:" + name);
		}

		override
        public IValue Add(Context context, IValue value)
        {
            return new Text(this.value.ToString() + value.ToString());
        }

        override
        public IValue Multiply(Context context, IValue value)
        {
            if (value is Integer)
            {
                int count = (int)((Integer)value).IntegerValue;
                if (count < 0)
                    throw new SyntaxError("Negative repeat count:" + count);
                if (count == 0)
                    return new Text("");
                if (count == 1)
                    return new Text(this.value.ToString());
                char[] cc = new char[count];
                for (int i = 0; i < count; i++)
                    cc[i] = this.value;
                return new Text(new String(cc));
          }
            else
               throw new SyntaxError("Illegal: Chararacter * " + value.GetType().Name);
         }

        public int CompareTo(Character obj)
        {
            return value.CompareTo(obj.Value);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Character)
                return this.value.CompareTo(((Character)value).value);
            else
                throw new SyntaxError("Illegal comparison: Character + " + value.GetType().Name);

        }

        
        override
        public Object ConvertTo(Type type)
        {
            return value;
        }
        
        override
        public String ToString()
        {
            return Value.ToString();
        }

        override
        public bool Equals(object obj)
        {
            if (obj is Character)
                return value.Equals(((Character)obj).value);
            else
                return value.Equals(obj);
        }

		override
		public bool Roughly(Context context, IValue obj)
		{
			if (obj is Character || obj is Text) 
			{
				return string.Compare(value.ToString(), obj.ToString(), true)==0;
			}
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
