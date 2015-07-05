using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.error;
using prompto.runtime;
using prompto.grammar;
using System.Collections;
using prompto.type;

namespace prompto.value
{
    public class Text : BaseValue, IComparable<Text>, IEnumerable<IValue>, ISliceable, IMultiplyable
    {
        string value;

        public Text(string value)
			: base(TextType.Instance)
        {
            this.value = value;
        }

        public string Value { get { return value; } }

        override
        public IValue Add(Context context, IValue value)
        {
            return new Text(this.value + value.ToString());
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
                    return new Text(this.value);
                char[] src = this.value.ToCharArray();
                char[] cc = new char[count * src.Length];
                for (int i = 0; i < count; i++)
                    Array.Copy(src, 0, cc, i * src.Length, src.Length);
                return new Text(new String(cc));
            }
            else
                throw new SyntaxError("Illegal: Chararacter * " + value.GetType().Name);
        }
        
        public int CompareTo(Text obj)
        {
            return value.CompareTo(obj.Value);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Text)
                return this.value.CompareTo(((Text)value).value);
            else
                throw new SyntaxError("Illegal comparison: Text + " + value.GetType().Name);
        }

		public IType ItemType {
			get { return CharacterType.Instance; }
		}

        public bool HasItem(Context context, IValue value)
        {
            if (value is Character)
                return this.value.IndexOf(((Character)value).Value) >= 0;
            else if (value is Text)
                return this.value.IndexOf(((Text)value).value) >= 0;
            else
                throw new SyntaxError("Illegal contain: Text + " + value.GetType().Name);
        }

  
        public IEnumerable<IValue> GetItems(Context context)
        {
			return (IEnumerable<IValue>)this; 
        }

  

        override
        public IValue GetMember(Context context, String name)
        {
            if ("length" == name)
                return new Integer(value.Length);
           else
                throw new NotSupportedException("No such member:" + name);
        }

        public IValue GetItem(Context context, IValue index)
        {
            try
            {
                if (index is Integer)
                    return new Character(value[(int)((Integer)index).IntegerValue - 1]);
                else
                    throw new NotSupportedException("No such item:" + index.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new IndexOutOfRangeError();
            }
   
       }

        override
       public Object ConvertTo(Type type)
        {
            return value;
        }
        
        public ISliceable Slice(Context context, Integer fi, Integer li)
        {
            int first = checkFirst(fi);
            int last = checkLast(li);
            return new Text(value.Substring(first - 1, last + 1 - first));
        }

        private int checkFirst(Integer fi)
        {
            int value = (fi == null) ? 1 : (int)fi.IntegerValue;
            if (value < 1 || value > this.Value.Length)
                throw new IndexOutOfRangeError();
            return value;
        }

        private int checkLast(Integer li)
        {
            int value = (li == null) ? this.Value.Length : (int)li.IntegerValue;
            if (value < 0)
                value = this.Value.Length + 1 + (int)li.IntegerValue;
            if (value < 1 || value > this.Value.Length)
                throw new IndexOutOfRangeError();
            return value;
        }

 
        override
        public String ToString()
        {
            return value;
        }

       IEnumerator IEnumerable.GetEnumerator()
        {
            return new CharacterEnumerator(value);
        }

        public IEnumerator<IValue> GetEnumerator()
        {
			return (IEnumerator<IValue>)new CharacterEnumerator(value);
        }

         override
        public bool Equals(object obj)
        {
            if (obj is Text)
                return value.Equals(((Text)obj).value);
            else
                return value.Equals(obj);
        }

		override
		public bool Roughly(Context context, IValue obj)
		{
			if (obj is Character || obj is Text) 
			{
				return string.Compare(value, obj.ToString(), true)==0;
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

    class CharacterEnumerator : IEnumerator<Character>
    {
        char[] chars;
        int idx = -1;

        public CharacterEnumerator(String value)
        {
            chars = value.ToCharArray();
        }

        public void Reset()
        {
            idx = -1;
        }

        public bool MoveNext()
        {
            return ++idx < chars.Length;
        }

		object IEnumerator.Current
		{
			get
			{
				return new Character(chars[idx]);
			}
		}

		Character IEnumerator<Character>.Current
        {
            get
            {
                return new Character(chars[idx]);
            }
        }

        public void Dispose()
        {
        }


    }


}