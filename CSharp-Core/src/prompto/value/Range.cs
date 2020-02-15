using prompto.error;
using prompto.runtime;
using System.Collections.Generic;
using System;
using System.Collections;
using prompto.value;
using Boolean = prompto.value.BooleanValue;
using prompto.grammar;
using prompto.expression;
using prompto.type;

namespace prompto.value
{

	public abstract class Range<T> : BaseValue, IEnumerable<IValue>, IRange where T : IValue
    {
	
	protected T low;
    protected T high;
	
	public Range(IType itemType, T left, T right) 
			: base(new RangeType(itemType))
	{
		// can't just use T : Comparable<T> because LocalDate and LocalTime extend Comparable<R>
		int cmp = compare(left,right);
		if(cmp<0) {
			this.low = left;
			this.high = right;
		} else {
			this.low = right;
			this.high = left;
		}
	}

    public override IValue GetItem(Context context, IValue index)
    {
        if (index is IntegerValue)
        {
            try
            {
                Object value = this.Item((IntegerValue)index);
                if (value is IExpression)
                    value = ((IExpression)value).interpret(context);
                if (value is IValue)
                    return (IValue)value;
                else
                    throw new InternalError("Item not a value!");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new IndexOutOfRangeError();
            }

        }
        else
            throw new NotSupportedException("No such item:" + index.ToString());
    }

    public IEnumerable<IValue> GetEnumerable(Context context)
    {
        return this;
    }
  
	override
    public String ToString() {
		return "[" + (low==null?"":low.ToString()) + ".." 
				+ (high==null?"":high.ToString()) + "]";
	}
	
	public T getLow() {
		return low;
	}
	
	public T getHigh() {
		return high;
	}

	override
    public bool Equals(Object obj) {
		if(!(obj is Range<T>))
            return false;
		Range<T> r = (Range<T>)obj;
		return low.Equals(r.low) && high.Equals(r.high);
	}

	public IType ItemType {
			get { return ((ContainerType)this.type).GetItemType (); }
	}

    public bool HasItem(Context context, IValue lval)
    {
		T val = (T)lval;
		return compare(val,low)>=0 && compare(high,val)>=0;
	}

   public ISliceable Slice(Context context, IntegerValue fi, IntegerValue li)
    {
		if (fi==null)
            fi = new IntegerValue(1L);
        if (fi.LongValue < 0)
			throw new IndexOutOfRangeError();
        if (li == null)
            li = size();
        if (li.LongValue < 0)
            li = new IntegerValue(size().LongValue + 1 + li.LongValue);
        else if (li.LongValue > size().LongValue)
			throw new IndexOutOfRangeError();
        return newInstance((T)Item(fi), (T)Item(li));
	}

    public IEnumerator<IValue> GetEnumerator() {
		return new RangeIterator<T>(this);
	}

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new RangeIterator<T>(this);
    }

		public bool Empty() 
		{
			return Length () == 0;
		}

		public abstract long Length ();

		public IntegerValue size()
		{
			return new IntegerValue(Length());
		}

    public abstract IValue Item(IntegerValue index);
	public abstract int compare(T o1,T o2);
	public abstract Range<T> newInstance(T left,T right);



}

	class RangeIterator<T> : IEnumerator<IValue> where T : IValue
{

    Range<T> range;
    Int64 index = 0L;

    public RangeIterator(Range<T> range)
    {
        this.range = range;
    }

    object Current
    {
        get { return range.Item(new IntegerValue(index)); }
    }

    public bool MoveNext()
    {
        long size = range.size().LongValue;
        return ++index <= size;
    }

    public void Reset()
    {
        index = 0L;
    }


    object IEnumerator.Current
    {
        get
        {
            return range.Item(new IntegerValue(index));
        }
    }

		IValue IEnumerator<IValue>.Current
    {
        get { return (T)range.Item(new IntegerValue(index)); }
    }


    public void Dispose()
    {
    }


}

}
