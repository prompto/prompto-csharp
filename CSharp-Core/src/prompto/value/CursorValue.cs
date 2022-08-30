using System.Collections.Generic;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.error;
using System;
using System.Collections;
using prompto.expression;

namespace prompto.value
{

	public class CursorValue : BaseValue, IIterable, IEnumerable<IValue>, IEnumerator<IValue>, IFilterable
	{

		Context context;
		IStoredEnumerator enumerator;

		public CursorValue (Context context, IType itemType, IStoredEnumerable documents)
			: base (new CursorType (itemType))
		{
			this.context = context;
			this.enumerator = documents.GetEnumerator();
			this.Mutable = itemType is CategoryType ? ((CategoryType)itemType).Mutable : false;
		}

		protected CursorValue(CursorValue source)
			: base(source.GetIType())
		{
			this.context = source.context;
			this.enumerator = source.enumerator;
			this.Mutable = source.Mutable;
		}


		public bool Mutable { get; set; }

		public bool Empty ()
		{
			return Length () == 0;
		}

		public long Length ()
		{
			return enumerator.Length;
		}

		public long TotalLength()
		{
			return enumerator.TotalLength;
		}

		public IEnumerable<IValue> GetEnumerable (Context context)
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this;
		}

		public IEnumerator<IValue> GetEnumerator ()
		{
			return this;
		}

		public virtual bool MoveNext ()
		{
			return enumerator.MoveNext ();
		}

		object IEnumerator.Current {
			get {
				return getCurrent ();
			}
		}

		public IValue Current
		{
			get {
				return getCurrent ();
			}
		}

		protected virtual IValue getCurrent() 
		{
			try 
			{
				IStored stored = enumerator.Current;
				CategoryType type = ReadItemType(stored);
				return type.newInstance(context, stored);
			} catch (PromptoError e) {
				throw new Exception (e.Message);
			}
		}

		CategoryType ReadItemType(IStored stored)
		{
			Object value = stored.GetData("category");
			if (value is List<String>)
			{
				List<String> categories = (List<String>)value;
				String category = categories[categories.Count - 1];
				CategoryType type = new CategoryType(category);
				type.Mutable = this.Mutable;
				return type;
			}
			else
				throw new InvalidDataError("Could not infer category!");
		}

		public ListValue ToListValue()
		{
			ListValue result = new ListValue(((CursorType)GetIType()).GetItemType());
			while (MoveNext())
				result.Add(Current);
			return result;
		}

		public void Dispose()
		{
			// nothing to do
		}

		public void Reset()
		{
			throw new Exception ("Unsupported!");
		}

		public override IValue GetMemberValue (Context context, string name, bool autoCreate)
		{
			if ("count".Equals (name))
				return new IntegerValue (Length ());
			else if ("totalCount".Equals(name))
				return new IntegerValue(TotalLength());
			else
				throw new InvalidDataError ("No such member:" + name);
		}

		public IFilterable Filter(Predicate<IValue> filter)
		{
			return new FilteredCursor(this, filter);
		}


	}

	public class FilteredCursor : CursorValue
	{
		Predicate<IValue> filter;
		IValue current;

		public FilteredCursor(CursorValue source, Predicate<IValue> filter)
			: base(source)
		{
			this.filter = filter;
		}

		public override bool MoveNext()
		{
			current = null;
			while (base.MoveNext())
			{
				current = base.getCurrent();
				if(filter.Invoke(current))
					return true;

			}
			current = null;
			return false;
		}

		protected override IValue getCurrent()
		{
			return current;
		}


		public override string ToString()
		{
			IType itemType = ((CursorType)GetIType()).GetItemType();
			ListValue values = new ListValue(itemType, (IEnumerable<IValue>)this, false);
			return values.ToString();
		}
	}


}
