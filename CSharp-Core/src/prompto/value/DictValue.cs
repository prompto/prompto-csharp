using prompto.runtime;
using System.Collections.Generic;
using System;
using System.Text;
using prompto.utils;
using Boolean = prompto.value.BooleanValue;
using prompto.value;
using prompto.error;
using prompto.expression;
using prompto.type;
using Newtonsoft.Json;
using prompto.store;

namespace prompto.value
{

    public class DictValue : Dictionary<TextValue, IValue>, IContainer, IEnumerable<IValue>
    {
        ContainerType type;
        bool mutable = false;

        public DictValue(IType itemType, bool mutable)
        {
            this.type = new DictType(itemType);
            this.mutable = mutable;
        }

        public DictValue(IType itemType, bool mutable, IDictionary<TextValue, IValue> from)
            : base(from)
        {
            this.type = new DictType(itemType);
            this.mutable = mutable;
        }

        public bool IsMutable()
        {
            return this.mutable;
        }

        public bool Empty()
        {
            return Length() == 0;
        }

        public long Length()
        {
            return this.Count;
        }

        public IType ItemType
        {
            get { return this.type.GetItemType(); }
        }

        public void SetIType(IType type)
        {
            this.type = (ContainerType)type;
        }

        public IType GetIType()
        {
            return type;
        }


        public void CollectStorables(List<IStorable> storables)
        {
            // nothing to do
        }

        public object GetStorableData()
        {
            throw new NotSupportedException("GetStorableData not supported by " + this.GetType().Name);
        }

        public IValue Swap(Context context)
        {
            DictValue swapped = new DictValue(TextType.Instance, true);
            foreach(KeyValuePair<TextValue, IValue> kvp in (Dictionary<TextValue, IValue>)this)
            {
                IValue key = kvp.Value;
                if (!(key is TextValue))
                    key = new TextValue(key.ToString());
                swapped[(TextValue)key] = kvp.Key;
            }
            swapped.mutable = false;
            return swapped;
        }

        public IValue Add(Context context, IValue value)
        {
            if (value is DictValue)
                return DictValue.merge(this, (DictValue)value);
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
            return new KVPEnumerator(this);
        }

       public virtual Int32 CompareTo(Context context, IValue value)
        {
            throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }

        public virtual bool HasItem(Context context, IValue value)
        {
            if (value is TextValue)
                return this.ContainsKey((TextValue)value);
            else
                throw new SyntaxError("Only Text key is supported by " + this.GetType().Name);
        }

        public virtual IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("count" == name)
                return new IntegerValue(this.Count);
            else if ("keys" == name)
            {
                HashSet<IValue> items = new HashSet<IValue>();
                foreach (TextValue item in this.Keys)
                    items.Add(item);
                return new SetValue(TextType.Instance, items);
            }
            else if ("values" == name)
            {
                ListValue list = new ListValue(type.GetItemType());
                list.AddRange(this.Values);
                return list;
            }
            else if ("text" == name)
                return new TextValue(this.ToString());
            else
                throw new NotSupportedException("No such member " + name);
        }

        public virtual void SetMemberValue(Context context, String name, IValue value)
        {
            throw new NotSupportedException("No such member:" + name);
        }

        public virtual IValue GetItem(Context context, IValue index)
        {
            if (index is TextValue)
            {
                IValue value;
                if (TryGetValue((TextValue)index, out value))
                    return value;
                else
                    return NullValue.Instance;
            }
            else
                throw new InvalidDataError("No such item:" + index.ToString());
        }

        public virtual void SetItem(Context context, IValue item, IValue value)
        {
            if (!(item is TextValue))
                throw new InvalidDataError("No such item:" + item.ToString());
            this[(TextValue)item] = value;
        }

        public virtual Object ConvertTo(Type type)
        {
            Dictionary<String, Object> dict = new Dictionary<string, object>();
            foreach (KeyValuePair<TextValue, IValue> kvp in ((Dictionary<TextValue, IValue>)this))
            {
                string key = (string)kvp.Key.ConvertTo(typeof(String));
                object value = kvp.Value.ConvertTo(typeof(Object));
                dict[key] = value;
            }
            return dict;
        }

        public static DictValue merge(DictValue dict1, DictValue dict2)
        {
            DictValue dict = new DictValue(dict1.type.GetItemType(), false, dict1); // TODO check type fungibility
            foreach (KeyValuePair<TextValue, IValue> kvp in ((Dictionary<TextValue, IValue>)dict2))
                dict[kvp.Key] = kvp.Value;
            return dict;
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            foreach (KeyValuePair<TextValue, IValue> kvp in (Dictionary<TextValue, IValue>)this)
            {
                sb.Append('"');
                sb.Append(kvp.Key.ToString());
                sb.Append('"');
                sb.Append(":");
                sb.Append(kvp.Value.ToString());
                sb.Append(", ");
            }
            if (sb.Length > 2)
                sb.Length = sb.Length - 2;
            else
                sb.Append(":");
            sb.Append(">");
            return sb.ToString();
        }

        public override bool Equals(object rval)
        {
            if (!(rval is DictValue))
                return false;
            DictValue dict = (DictValue)rval;
            if (this.Count != dict.Count)
                return false;
            foreach (TextValue key in this.Keys)
            {
                if (!dict.ContainsKey(key))
                    return false;
                Object lival = this[key];
                Object rival = dict[key];
                if (!lival.Equals(rival))
                    return false;
            }
            return true;
        }

        public bool Equals(Context context, IValue rval)
        {
            if (!(rval is DictValue))
                return false;
            DictValue dict = (DictValue)rval;
            if (this.Count != dict.Count)
                return false;
            foreach (TextValue key in this.Keys)
            {
                if (!dict.ContainsKey(key))
                    return false;
                Object lival = this[key];
                if (lival is IExpression)
                    lival = ((IExpression)lival).interpret(context);
                Object rival = dict[key];
                if (rival is IExpression)
                    rival = ((IExpression)rival).interpret(context);
                if (lival is IValue && rival is IValue)
                {
                    if (!((IValue)lival).Equals(context, (IValue)rival))
                        return false;
                }
                else if (!lival.Equals(rival))
                    return false;
            }
            return true;
        }

        public bool Roughly(Context context, IValue rval)
        {
            return this.Equals(context, rval);
        }

        public bool Contains(Context context, IValue rval)
        {
            return false;
        }


        public virtual void ToJson(Context context, JsonWriter generator, Object instanceId, String fieldName, bool withType, Dictionary<String, byte[]> binaries)
        {
            throw new NotSupportedException("No ToJson support for " + this.GetType().Name);
        }

        public IValue ToDocumentValue(Context context)
        {
            throw new NotSupportedException("Yet!"); 
        }

    }

    public class KVPValue : BaseValue
    {
        KeyValuePair<TextValue, IValue> kvp;

        public KVPValue(KeyValuePair<TextValue, IValue> kvp)
            : base(null) // TODO check that this is safe
        {
            this.kvp = kvp;
        }

        override
        public IValue GetMemberValue(Context context, String name, bool autoCreate)
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
        IEnumerator<KeyValuePair<TextValue, IValue>> src;

        public KVPEnumerator(DictValue dict)
        {
            this.src = ((Dictionary<TextValue, IValue>)dict).GetEnumerator();
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