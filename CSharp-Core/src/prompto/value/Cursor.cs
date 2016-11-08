using System.Collections.Generic;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.error;
using System;
using System.Collections;


namespace prompto.value
{

	public class Cursor : BaseValue, IIterable, IEnumerable<IValue>, IEnumerator<IValue>
	{

		Context context;
		IStoredEnumerator documents;

		public Cursor (Context context, IType itemType, IStoredEnumerable documents)
			: base (new CursorType (itemType))
		{
			this.context = context;
			this.documents = documents.GetEnumerator();
		}

		public bool Mutable { get; set; }

		public bool Empty ()
		{
			return Length () == 0;
		}

		public long Length ()
		{
			return documents.Length;
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

		public bool MoveNext ()
		{
			return documents.MoveNext ();
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

		IValue getCurrent() 
		{
			try 
			{
				IStored stored = documents.Current;
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

		public void Dispose()
		{
			// nothing to do
		}

		public void Reset()
		{
			throw new Exception ("Unsupported!");
		}

		public override IValue GetMember (Context context, string name, bool autoCreate)
		{
			if ("count".Equals (name))
				return new Integer (Length ());
			else
				throw new InvalidDataError ("No such member:" + name);
		}



	}

}
