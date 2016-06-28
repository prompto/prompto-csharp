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
		IDocumentEnumerator documents;

		public Cursor (Context context, IType itemType, IDocumentEnumerator documents)
			: base (new CursorType (itemType))
		{
			this.context = context;
			this.documents = documents;
		}

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
			try {
				Document doc = documents.Current;
				CategoryType itemType = (CategoryType)((ContainerType)type).GetItemType ();
				return itemType.newInstance (context, doc);
			} catch (PromptoError e) {
				throw new Exception (e.Message);
			}
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
