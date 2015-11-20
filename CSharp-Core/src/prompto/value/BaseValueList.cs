using prompto.error;
using prompto.runtime;
using System.Collections.Generic;
using System;
using System.Text;
using prompto.utils;
using prompto.value;
using Boolean = prompto.value.Boolean;
using prompto.grammar;
using prompto.expression;
using prompto.type;

namespace prompto.value
{

    public abstract class BaseValueList<T> : List<IValue>, ISliceable where T : BaseValueList<T>
    {

		ContainerType type;

		protected BaseValueList(ContainerType type)
	    {
			this.type = type;
        }

		protected BaseValueList(ContainerType type, IValue item)
	    {
			this.type = type;
			this.Add(item);
        }

		protected BaseValueList(ContainerType type, List<IValue> items)
		{
			this.type = type;
			this.AddRange(items);
		}

		public bool Empty()
		{
			return Length() == 0;
		}

		public long Length ()
		{
			return this.Count;
		}


		public virtual bool IsMutable()
		{
			return false;
		}

		public void SetType(ContainerType type)
		{
			this.type = type;
		}

		public IType GetType(Context context) {
			return type;
		}

		public IType ItemType {
			get { return ((ContainerType)type).GetItemType (); }
		}

       /* for unified grammar */
        public void add(IValue value)
        {
            this.Add(value);
        }
       
        public virtual IValue Add(Context context, IValue value)
        {
            throw new NotSupportedException("Add not supported by " + this.GetType().Name);
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

        public virtual Int32 CompareTo(Context context, IValue value)
        {
            throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }

        public bool HasItem(Context context, IValue value)
        {
            return this.Contains(value);
        }

        public IEnumerable<IValue> GetEnumerable(Context context)
        { 
            return this; 
        }

		public virtual IValue GetMember(Context context, String name, bool autoCreate)
        {
            if ("length" == name)
                return new Integer(this.Count);
            else
                throw new NotSupportedException("No such member:" + name);
        }

		public virtual void SetMember(Context context, String name, IValue value)
		{
			throw new NotSupportedException("No such member:" + name);
		}

		public virtual IValue GetItem(Context context, IValue index)
        {
            if (index is Integer)
            {
                try
                {
                    return this[(int)((Integer)index).IntegerValue - 1];
               }
                catch (ArgumentOutOfRangeException)
                {
                    throw new IndexOutOfRangeError();
                }
   
            }
            else
                throw new NotSupportedException("No such item:" + index.ToString());
        }


        public ISliceable Slice(Context context, Integer fi, Integer li)
        {
            long fi_ = (fi == null) ? 1L : fi.IntegerValue;
            if (fi_ < 0)
                throw new IndexOutOfRangeError();
            long li_ = (li == null) ? (long)Count : li.IntegerValue;
            if (li_ < 0)
                li_ = Count + 1 + li_;
            else if (li_ > Count)
                throw new IndexOutOfRangeError();
            T result = newInstance();
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

        public virtual Object ConvertTo(Type type)
        {
            return this;
        }
 
        protected abstract T newInstance();

        public T merge(ListValue other)
        {
            T list = this.newInstance();
            list.AddRange(this);
            list.AddRange(other);
            return list;
        }

        public T merge(TupleValue other)
        {
            T list = this.newInstance();
            list.AddRange(this);
            list.AddRange(other);
            return list;
        }

        public Boolean Contains(Context context, Object lval)
        {
            foreach (Object o in this)
            {
                if (o.Equals(lval))
                    return Boolean.TRUE;
            }
            return Boolean.FALSE;
        }

		public virtual bool Equals (Context context, IValue lval)
		{
			return this.Equals (lval);
		}

		public virtual bool Roughly (Context context, IValue lval)
		{
			return this.Equals (context, lval);
		}

		override
        public  String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Object o in this)
            {
                sb.Append(o.ToString());
                sb.Append(", ");
            }
            if(sb.Length>2)
                sb.Length = sb.Length - 2;
            return sb.ToString();
        }

    }

}
