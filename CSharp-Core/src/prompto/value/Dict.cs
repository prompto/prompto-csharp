using prompto.runtime;
using System.Collections.Generic;
using System;
using System.Text;
using prompto.utils;
using Boolean = prompto.value.Boolean;
using prompto.value;
using prompto.error;
using prompto.expression;
using prompto.type;

namespace prompto.value
{

	public class Dict : Dictionary<Text, IValue>, IContainer, IEnumerable<IValue>
    {
		ContainerType type;

		public Dict(IType itemType)
	    {
			this.type = new DictType (itemType);
	    }

		public Dict(IType itemType, IDictionary<Text, IValue> from)
            : base(from)
        {
			this.type = new DictType (itemType);
        }

		public bool Empty()
		{
			return Length () == 0;
		}

		public long Length()
		{
			return this.Count;
		}

		public bool IsMutable()
		{
			return false;
		}

		public IType ItemType
		{
			get { return this.type.GetItemType (); }
		}

		public IType GetType(Context context)
		{
			return type;
		}

        public IValue Add(Context context, IValue value)
        {
            if (value is Dict)
                return Dict.merge(this, (Dict)value);
            else
                throw new SyntaxError("Illegal: Dict + " + value.GetType().Name);
        }

        public virtual IValue Subtract(Context context, IValue value)
        {
            throw new NotSupportedException("Subtract not supported by " + this.GetType().Name);
        }

        public virtual IValue Multiply(Context context, IValue value)
        {
            throw new NotSupportedException("Multiply not supported by " + this.GetType().Name);
        }

        public virtual IValue Divide(Context context, IValue value)
        {
            throw new NotSupportedException("Divide not supported by " + this.GetType().Name);
        }

        public virtual IValue IntDivide(Context context, IValue value)
        {
            throw new NotSupportedException("IntDivide not supported by " + this.GetType().Name);
        }

        public virtual IValue Modulo(Context context, IValue value)
        {
            throw new NotSupportedException("Modulo not supported by " + this.GetType().Name);
        }

		public IEnumerable<IValue> GetEnumerable(Context context)
        { 
			return this; 
        }

		public new IEnumerator<IValue> GetEnumerator()
		{
			return new KVPEnumerator (this);
		}

        public virtual Int32 CompareTo(Context context, IValue value)
        {
            throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }

        public virtual bool HasItem(Context context, IValue value)
        {
            if (value is Text)
                return this.ContainsKey((Text)value);
            else
                throw new SyntaxError("Only Text key is supported by " + this.GetType().Name);
        }

		public virtual IValue GetMember(Context context, String name, bool autoCreate)
        {
            if ("length" == name)
                return new Integer(this.Count);
            else if ("keys" == name)
            {
				ListValue list = new ListValue(TextType.Instance);
                list.AddRange(this.Keys);
                return list;
            }
            else if ("values" == name)
            {
				ListValue list = new ListValue(type.GetItemType());
                list.AddRange(this.Values);
                return list;
            }
            else
                throw new NotSupportedException("No such member:" + name);
        }

		public virtual void SetMember(Context context, String name, IValue value)
		{
			throw new NotSupportedException("No such member:" + name);
		}

		public virtual IValue GetItem(Context context, IValue index)
        {
            if (index is Text)
            {
				IValue value;
				TryGetValue ((Text)index, out value);
				return value;
            }
            else
                throw new NotSupportedException("No such item:" + index.ToString());
        }

        public virtual Object ConvertTo(Type type)
        {
            return this;
        }
 
        public static Dict merge(Dict dict1, Dict dict2)
        {
			Dict dict = new Dict(dict1.type.GetItemType(), dict1); // TODO check type fungibility
			foreach (KeyValuePair<Text, IValue> kvp in ((Dictionary<Text, IValue>)dict2))
                dict[kvp.Key] = kvp.Value;
			return dict;
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
			foreach (KeyValuePair<Text, IValue> kvp in ((Dictionary<Text, IValue>)this))
            {
                sb.Append(kvp.Key.ToString());
                sb.Append(":");
                sb.Append(kvp.Value.ToString());
                sb.Append(", ");
            }
            if(sb.Length>2)
                sb.Length = sb.Length - 2;
            sb.Append("}");
            return sb.ToString();
        }

		public bool Equals(Context context, IValue rval)
		{
			if (!(rval is Dict))
				return false;
			Dict dict = (Dict)rval;
			if (this.Count != dict.Count)
				return false;
			foreach (Text key in this.Keys)
			{
				if (!dict.ContainsKey(key))
					return false;
				Object lival = this[key];
				if (lival is IExpression)
					lival = ((IExpression)lival).interpret(context);
				Object rival = dict[key];
				if (rival is IExpression)
					rival = ((IExpression)rival).interpret(context);
				if (lival is IValue && rival is IValue) {
					if (!((IValue)lival).Equals (context, (IValue)rival))
						return false;
				} else if (!lival.Equals(rival))
					return false;
			}
			return true;
		}

		public bool Roughly(Context context, IValue rval)
		{
			return this.Equals (context, rval);
		}

     }

    public class KVPValue : BaseValue
    {
		KeyValuePair<Text, IValue> kvp;

		public KVPValue(KeyValuePair<Text, IValue> kvp)
			: base(null) // TODO check that this is safe
        {
            this.kvp = kvp;
        }

        override
		public IValue GetMember(Context context, String name, bool autoCreate)
        {
            if ("key" == name)
                return kvp.Key;
            else if ("value" == name)
				return kvp.Value;
            else
                throw new NotSupportedException("No such member:" + name);
        }
    }

	class KVPEnumerator : IEnumerator<IValue> 
    {
        KVPValue current = null;
		IEnumerator<KeyValuePair<Text, IValue>> src;

			public KVPEnumerator(Dict dict)
        {
			this.src = ((Dictionary<Text, IValue>)dict).GetEnumerator();
        }

		public object Current { get { return current; } }

		IValue IEnumerator<IValue>.Current { get { return current; } }

        public bool MoveNext()
        {
            current = null;
            bool res = src.MoveNext();
            if (res)
                current = new KVPValue(src.Current);
            return res;
        }

        public void Reset()
        {
            src.Reset();
            current = null;
        }

        public void Dispose()
        {
            src.Dispose();
        }

    }
}