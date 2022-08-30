using System;
using prompto.value;
using prompto.error;
using prompto.runtime;
using prompto.grammar;
using System.Collections.Generic;
using prompto.expression;
using prompto.type;
using Newtonsoft.Json;
using prompto.store;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;

namespace prompto.value
{


    public class ListValue : List<IValue>, ISliceable, IContainer, IFilterable, IMultiplyable
    {
        List<Object> storables;
        ListType type;
        bool mutable = false;

        public ListValue(IType itemType)
            : this(itemType, new List<IValue>())
        {
        }

        public ListValue(IType itemType, IValue value)
            : this(itemType, value, false)
        {
        }

        public ListValue(IType itemType, IValue value, bool mutable)
        {
            type = new ListType(itemType);
            Add(value);
            this.mutable = mutable;
        }

        public ListValue(IType itemType, IEnumerable<IValue> values)
            : this(itemType, values, false)
        {
        }

        public ListValue(IType itemType, IEnumerable<IValue> values, bool mutable)
        {
            type = new ListType(itemType);
            AddRange(values);
            this.mutable = mutable;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (IValue o in this)
            {
                sb.Append(o.ToString());
                sb.Append(", ");
            }
            if (sb.Length > 2)
                sb.Length = sb.Length - 2;
            sb.Append("]");
            return sb.ToString();
        }

        public JToken ToJsonToken()
        {
            JArray token = new JArray();
            foreach (IValue o in this)
                token.Add(o.ToJsonToken());
            return token;
        }

        public void ToJson(Context context, JsonWriter generator, Object instanceId, String fieldName, bool withType, Dictionary<String, byte[]> binaries)
        {
            throw new NotSupportedException("No ToJson support for " + this.GetType().Name);
        }

        public bool IsMutable()
        {
            return this.mutable;
        }

        public void SetIType(IType type)
        {
            this.type = (ListType)type;
        }

        public IType GetIType()
        {
            return type;
        }

        public IType ItemType
        {
            get { return type.GetItemType(); }
        }

        public Object ConvertTo(Context context, Type type)
        {
            return this;
        }


        public IEnumerable<IValue> GetEnumerable(Context context)
        {
            return this;
        }


        public void CollectStorables(List<IStorable> storables)
        {
            this.ForEach((item) => item.CollectStorables(storables));
        }

        public Object GetStorableData()
        {
            if (storables == null)
            {
                storables = new List<Object>();
                this.ForEach((item) => storables.Add(item.GetStorableData()));
            }
            return storables;
        }



        public bool Empty()
        {
            return Count == 0;
        }

        public long Length()
        {
            return this.Count;
        }


        public override bool Equals(object rval)
        {
            if (!(rval is ListValue))
                return false;
            ListValue list = (ListValue)rval;
            if (this.Count != list.Count)
                return false;
            IEnumerator<Object> li = this.GetEnumerator();
            IEnumerator<Object> ri = list.GetEnumerator();
            while (li.MoveNext() && ri.MoveNext())
            {
                Object lival = li.Current;
                Object rival = ri.Current;
                if (!lival.Equals(rival))
                    return false;
            }
            return true;
        }

        public bool Equals(Context context, IValue rval)
        {
            if (!(rval is ListValue))
                return false;
            ListValue list = (ListValue)rval;
            if (this.Count != list.Count)
                return false;
            IEnumerator<Object> li = this.GetEnumerator();
            IEnumerator<Object> ri = list.GetEnumerator();
            while (li.MoveNext() && ri.MoveNext())
            {
                Object lival = li.Current;
                if (lival is IExpression)
                    lival = ((IExpression)lival).interpret(context);
                Object rival = ri.Current;
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

        public bool Roughly(Context context, IValue lval)
        {
            return this.Equals(context, lval); // TODO
        }


        public bool Contains(Context context, IValue rval)
        {
            return false;
        }



        public Int32 CompareTo(Context context, IValue value)
        {
            throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }



        public IValue Add(Context context, IValue value)
        {
            if (value is ListValue)
            {
                ListValue result = new ListValue(type.GetItemType(), new List<IValue>(), mutable) ;
                result.AddRange(this);
                result.AddRange((ListValue)value);
                return result;
            }
            else if (value is SetValue)
            {
                ListValue result = new ListValue(type.GetItemType(), new List<IValue>(), mutable);
                result.AddRange(this);
                result.AddRange(((SetValue)value).getItems());
                return result;
            }
            else
                throw new SyntaxError("Illegal : List + " + value.GetType().Name);
        }

      public IValue Subtract(Context context, IValue value)
        {
            if (value is ListValue)
            {
                SetValue set = new SetValue(this.ItemType);
                value = set.Add(context, value);
            }
            if (value is SetValue)
            {
                ListValue result = new ListValue(ItemType);
                result.AddRange(this);
                result.RemoveAll(item => ((SetValue)value).HasItem(context, item));
                return result;
            }
            else
                throw new SyntaxError("Illegal : List - " + value.GetType().Name);
        }


        public IValue Multiply(Context context, IValue value)
        {
            if (value is IntegerValue)
            {
                int count = (int)((IntegerValue)value).LongValue;
                if (count < 0)
                    throw new SyntaxError("Negative repeat count:" + count);
                if (count == 0)
                    return new ListValue(this.ItemType);
                if (count == 1)
                    return this;
                ListValue result = new ListValue(this.ItemType);
                for (long i = 0; i < count; i++)
                    result.AddRange(this); // TODO: interpret items ?
                return result;
            }
            else
                throw new SyntaxError("Illegal: List * " + value.GetType().Name);
        }

        public IValue Divide(Context context, IValue value)
        {
            throw new NotSupportedException("Divide not supported by " + this.GetType().Name);
        }

        public IValue IntDivide(Context context, IValue value)
        {
            throw new NotSupportedException("IntDivide not supported by " + this.GetType().Name);
        }

        public IValue Modulo(Context context, IValue value)
        {
            throw new NotSupportedException("Modulo not supported by " + this.GetType().Name);
        }

        public IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("count" == name)
                return new IntegerValue(this.Count);
            else if ("text" == name)
                return new TextValue(this.ToString());
            else if ("json" == name)
            {
                JToken token = ToJsonToken();
                JsonSerializer serializer = JsonSerializer.CreateDefault();
                StringWriter writer = new StringWriter();
                serializer.Serialize(writer, token);
                return new TextValue(writer.ToString());
            }
            else
                throw new NotSupportedException("No such member " + name);
        }

        public void SetMemberValue(Context context, String name, IValue value)
        {
            throw new NotSupportedException("No such member:" + name);
        }

        public bool HasItem(Context context, IValue value)
        {
            return this.Contains(value);
        }

        public virtual IValue GetItem(Context context, IValue index)
        {
            if (index is IntegerValue)
            {
                try
                {
                    return this[(int)((IntegerValue)index).LongValue - 1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new IndexOutOfRangeError();
                }


            }
            else
                throw new InvalidDataError("No such item:" + index.ToString());
        }

        public virtual void SetItem(Context context, IValue item, IValue value)
        {
            if (!(item is IntegerValue))
                throw new InvalidDataError("No such item:" + item.ToString());
            int index = (int)((IntegerValue)item).LongValue;
            try
            {
                this[index - 1] = value;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new IndexOutOfRangeError();
            }
        }

        public ISliceable Slice(Context context, IntegerValue fi, IntegerValue li)
        {
            long fi_ = (fi == null) ? 1L : fi.LongValue;
            if (fi_ < 0)
                throw new IndexOutOfRangeError();
            long li_ = (li == null) ? (long)Count : li.LongValue;
            if (li_ < 0)
                li_ = Count + 1 + li_;
            else if (li_ > Count)
                throw new IndexOutOfRangeError();
            ListValue result = new ListValue(type.GetItemType());
            long idx = 0;
            foreach (IValue e in this)
            {
                if (++idx < fi_)
                    continue;
                if (idx > li_)
                    break;
                result.Add(e);
            }
            return result;
        }

        public IFilterable Filter(Predicate<IValue> filter)
        {
            List<IValue> items = FindAll(filter);
            return new ListValue(type.GetItemType(), items);
        }

        public IValue ToDocumentValue(Context context)
        {
            List<IValue> items = new List<IValue>();
            foreach (IValue item in this)
            {
                items.Add(item.ToDocumentValue(context));
            }
            return new ListValue(AnyType.Instance, items);
        }

        public IValue ToSetValue()
        {
            HashSet<IValue> items = new HashSet<IValue>(this);
            return new SetValue(type.GetItemType(), items);
        }


    }
}
