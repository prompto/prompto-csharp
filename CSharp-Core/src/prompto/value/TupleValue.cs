using System;
using prompto.value;
using prompto.error;
using prompto.runtime;
using prompto.grammar;
using prompto.utils;
using prompto.type;
using System.Collections.Generic;
using System.Text;
using prompto.store;
using prompto.expression;
using Newtonsoft.Json;

namespace prompto.value
{

    public class TupleValue : List<IValue>, ISliceable, IContainer, IMultiplyable
    {
        List<Object> storables;
        bool mutable = false;

        public TupleValue()
        {
        }

        public TupleValue(IValue item)
        {
            Add(item);
        }

        public TupleValue(List<IValue> items)
        {
            AddRange(items);
        }

        public TupleValue(List<IValue> items, bool mutable)
        {
            AddRange(items);
            this.mutable = mutable;
        }


        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (Object o in this)
            {
                sb.Append(o.ToString());
                sb.Append(", ");
            }
            if (sb.Length > 2)
                sb.Length = sb.Length - 2;
            sb.Append(")");
            return sb.ToString();
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
            if (type != TupleType.Instance)
                throw new InternalError("Can't set Tuple type!");
        }

        public IType GetIType()
        {
            return TupleType.Instance;
        }

        public IEnumerable<IValue> GetEnumerable(Context context)
        {
            return this;
        }

        public virtual Object ConvertTo(Type type)
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

        public bool Equals(Context context, IValue lval)
        {
            return this.Equals(lval);
        }

        public bool Roughly(Context context, IValue lval)
        {
            return this.Equals(context, lval);
        }


        public bool Contains(Context context, IValue rval)
        {
            return false;
        }


        public IValue Add(Context context, IValue value)
        {
            if (value is TupleValue)
            {
                TupleValue result = new TupleValue();
                result.AddRange(this);
                result.AddRange((TupleValue)value);
                return result;
            }
            else if (value is ListValue)
            {
                TupleValue result = new TupleValue();
                result.AddRange(this);
                result.AddRange((ListValue)value);
                return result;
            }
            else if (value is SetValue)
            {
                TupleValue result = new TupleValue();
                result.AddRange(this);
                result.AddRange(((SetValue)value).getItems());
                return result;
            }
            else
                throw new SyntaxError("Illegal: Tuple + " + value.GetType().Name);
        }

        public IValue Subtract(Context context, IValue value)
        {
            throw new NotSupportedException("Subtract not supported by " + this.GetType().Name);
        }

        public IValue Multiply(Context context, IValue value)
        {
            throw new NotSupportedException("Multiply not supported by " + this.GetType().Name);
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

        public IValue GetItem(Context context, IValue index)
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

        public void SetItem(Context context, IValue item, IValue value)
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
            TupleValue result = new TupleValue();
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

        public void ToDialect(CodeWriter writer)
        {
            writer.append('(');
            if (this.Count > 0)
            {
                foreach (Object o in this)
                {
                    if (o is IDialectElement)
                        ((IDialectElement)o).ToDialect(writer);
                    else
                        writer.append(o.ToString());
                    writer.append(", ");
                }
                writer.trimLast(2);
            }
            writer.append(')');
        }


        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is TupleValue)
                return CompareTo(context, (TupleValue)value, new List<bool>());
            else
                throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }

        public Int32 CompareTo(Context context, TupleValue other, List<bool> directions)
        {
            IEnumerator<bool> iterDirs = directions.GetEnumerator();
            IEnumerator<IValue> iterThis = this.GetEnumerator();
            IEnumerator<IValue> iterOther = other.GetEnumerator();
            while (iterThis.MoveNext())
            {
                bool descending = iterDirs.MoveNext() ? iterDirs.Current : false;
                if (iterOther.MoveNext())
                {
                    // compare items
                    IValue thisVal = iterThis.Current;
                    IValue otherVal = iterOther.Current;
                    if (thisVal == null && otherVal == null)
                        continue;
                    else if (thisVal == null)
                        return descending ? 1 : -1;
                    else if (otherVal == null)
                        return descending ? -1 : 1;
                    int cmp = thisVal.CompareTo(context, otherVal);
                    // if not equal, done
                    if (cmp != 0)
                        return descending ? -cmp : cmp;
                }
                else
                    return descending ? -1 : 1;
            }
            bool desc2 = iterDirs.MoveNext() ? iterDirs.Current : false;
            if (iterOther.MoveNext())
                return desc2 ? 1 : -1;
            else
                return 0;
        }
    }

}
