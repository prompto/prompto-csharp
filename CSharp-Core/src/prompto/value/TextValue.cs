using System;
using System.Collections.Generic;
using prompto.error;
using prompto.runtime;
using prompto.grammar;
using System.Collections;
using prompto.type;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace prompto.value
{
    public class TextValue : BaseValue, IComparable<TextValue>, IEnumerable<IValue>, ISliceable, IMultiplyable
    {
        string value;

        public TextValue(string value)
            : base(TextType.Instance)
        {
            this.value = value;
        }

        public string Value { get { return value; } }


        public override object GetStorableData()
        {
            return value;
        }


        public override IValue Add(Context context, IValue value)
        {
            return new TextValue(this.value + value.ToString());
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
                    return new TextValue(this.value);
                char[] src = this.value.ToCharArray();
                char[] cc = new char[count * src.Length];
                for (int i = 0; i < count; i++)
                    Array.Copy(src, 0, cc, i * src.Length, src.Length);
                return new TextValue(new string(cc));
            }
            else
                throw new SyntaxError("Illegal: Chararacter * " + value.GetType().Name);
        }

        public int CompareTo(TextValue obj)
        {
            return value.CompareTo(obj.Value);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is TextValue)
                return this.value.CompareTo(((TextValue)value).value);
            else
                throw new SyntaxError("Illegal comparison: Text + " + value.GetType().Name);
        }

        public IType ItemType
        {
            get { return CharacterType.Instance; }
        }

        public bool HasItem(Context context, IValue value)
        {
            if (value is CharacterValue)
                return this.value.IndexOf(((CharacterValue)value).Value) >= 0;
            else if (value is TextValue)
                return this.value.IndexOf(((TextValue)value).value) >= 0;
            else
                throw new SyntaxError("Illegal contain: Text + " + value.GetType().Name);
        }


        public IEnumerable<IValue> GetEnumerable(Context context)
        {
            return (IEnumerable<IValue>)this;
        }

        public bool Empty()
        {
            return Length() == 0;
        }

        public long Length()
        {
            return value.Length;
        }



        public override IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("count" == name)
                return new IntegerValue(value.Length);
            else
                return base.GetMemberValue(context, name, autoCreate);
        }

        public override IValue GetItem(Context context, IValue index)
        {
            try
            {
                if (index is IntegerValue)
                    return new CharacterValue(value[(int)((IntegerValue)index).LongValue - 1]);
                else
                    throw new NotSupportedException("No such item:" + index.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new IndexOutOfRangeError();
            }

        }

        
       public override Object ConvertTo(Type type)
        {
            return value;
        }

        public ISliceable Slice(Context context, IntegerValue fi, IntegerValue li)
        {
            int first = checkFirst(fi);
            int last = checkLast(li);
            return new TextValue(value.Substring(first - 1, last + 1 - first));
        }

        private int checkFirst(IntegerValue fi)
        {
            int value = (fi == null) ? 1 : (int)fi.LongValue;
            if (value < 1 || value > this.Value.Length)
                throw new IndexOutOfRangeError();
            return value;
        }

        private int checkLast(IntegerValue li)
        {
            int value = (li == null) ? this.Value.Length : (int)li.LongValue;
            if (value < 0)
                value = this.Value.Length + 1 + (int)li.LongValue;
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


        public override bool Equals(object obj)
        {
            if (obj is TextValue)
                return value.Equals(((TextValue)obj).value);
            else
                return value.Equals(obj);
        }


        public override bool Roughly(Context context, IValue obj)
        {
            if (obj is CharacterValue || obj is TextValue)
            {
                return string.Compare(value, obj.ToString(), true) == 0;
            }
            else
                return value.Equals(obj);
        }

        public override bool Contains(Context context, IValue obj)
        {
            if (obj is TextValue)
                return value.Contains(((TextValue)obj).Value);
            else if (obj is CharacterValue)
                return value.IndexOf(((CharacterValue)obj).Value) >= 0;
            else
                return false;
        }


        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override void ToJson(Context context, JsonWriter generator, object instanceId, String fieldName, bool withType, Dictionary<string, byte[]> binaries)
        {
            generator.WriteValue(value);
        }

        public override JToken ToJsonToken()
        {
            return new JValue(value);
        }

    }

    class CharacterEnumerator : IEnumerator<CharacterValue>
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
                return new CharacterValue(chars[idx]);
            }
        }

        CharacterValue IEnumerator<CharacterValue>.Current
        {
            get
            {
                return new CharacterValue(chars[idx]);
            }
        }

        public void Dispose()
        {
        }


    }


}
