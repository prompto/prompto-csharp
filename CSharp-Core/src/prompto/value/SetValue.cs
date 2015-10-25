using prompto.type;
using System.Collections.Generic;
using prompto.runtime;
using prompto.error;
using System;
using System.Text;

namespace prompto.value
{

	public class SetValue : BaseValue, IContainer
	{

		HashSet<IValue> items = null;

		public SetValue (IType itemType)
			: base (new SetType (itemType))
		{
			this.items = newSet ();
		}

		public SetValue (IType itemType, HashSet<IValue> items)
			: base (new SetType (itemType))
		{
			this.items = items;
		}

		protected HashSet<IValue> newSet ()
		{
			return new HashSet<IValue> ();
		}

		public bool isEmpty ()
		{
			return items.Count==0;
		}

		public long length ()
		{
			return items.Count;
		}

		public ICollection<IValue> getItems ()
		{
			return items;
		}

		public bool HasItem (Context context, IValue value)
		{
			return items.Contains (value);
		}

		public IValue GetItem (Context context, IValue index)
		{
			if (index is Integer) {
				int idx = (int)((Integer)index).IntegerValue - 1;
				return getNthItem (idx);
			} else
				throw new SyntaxError ("No such item:" + index.ToString ());
		}

		private IValue getNthItem (int idx)
		{
			IEnumerator<IValue> it = items.GetEnumerator();
			while (it.MoveNext()) {
				IValue item = it.Current;
				if (idx-- == 0)
					return item;
			}
			throw new IndexOutOfRangeError ();
		}

		public IType ItemType {
			get { return ((ContainerType)this.type).GetItemType (); }
		}

		public IEnumerable<IValue> GetItems (Context context)
		{
			return items;
		}

		public override bool Equals (Object obj)
		{
			if (!(obj is SetValue))
				return false;
			return items.SetEquals (((SetValue)obj).items);
		}

		public SetValue newInstance (IEnumerable<IValue> values)
		{
			IType itemType = ((SetType)type).GetItemType ();
			SetValue result = new SetValue (itemType);
			foreach(IValue value in values)
				result.items.Add (value);
			return result;
		}

		public override String ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("<");
			if (this.items.Count > 0) {
				foreach (IValue value in this.items) {
					sb.Append (value.ToString ());
					sb.Append (", ");
				}
				sb.Length -= 2;
			}
			sb.Append (">");
			return sb.ToString ();
		}

		public override IValue Add (Context context, IValue value)
		{
			if (value is IContainer)
				return this.merge (((IContainer)value).GetItems (context));
			else if (value is SetValue)
				return this.merge (((SetValue)value).GetItems (context));
			else
				throw new SyntaxError ("Illegal: " + this.type.GetName () + " + " + value.GetType().Name);
		}

		public SetValue merge (IEnumerable<IValue> items)
		{
			HashSet<IValue> result = new HashSet<IValue>  ();
			foreach(IValue value in this.items)
				result.Add(value);
			foreach(IValue value in items)
				result.Add(value);
			return newInstance (result);
		}

	}
}