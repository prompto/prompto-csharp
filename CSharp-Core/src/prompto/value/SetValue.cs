using prompto.type;
using System.Collections.Generic;
using prompto.runtime;
using prompto.error;
using System;
using System.Text;
using prompto.expression;
using Newtonsoft.Json.Linq;

namespace prompto.value
{

	public class SetValue : BaseValue, IContainer, IFilterable
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

		public bool Empty ()
		{
			return items.Count==0;
		}

		public long Length ()
		{
			return items.Count;
		}

		public IEnumerable<IValue> GetEnumerable(Context context)
		{
			return items;
		}

		public ICollection<IValue> getItems ()
		{
			return items;
		}

		public bool HasItem (Context context, IValue value)
		{
			return items.Contains (value);
		}

		public override IValue GetItem (Context context, IValue index)
		{
			if (index is IntegerValue) {
				int idx = (int)((IntegerValue)index).LongValue - 1;
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
			SetValue result = new SetValue (ItemType);
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
				return this.merge (((IContainer)value).GetEnumerable (context));
			else if (value is SetValue)
				return this.merge (((SetValue)value).GetItems (context));
			else
				throw new SyntaxError ("Illegal: " + this.type.GetTypeName () + " + " + value.GetType().Name);
		}


		public override IValue Subtract(Context context, IValue value)
		{
			if (value is ListValue)
			{
				SetValue set = new SetValue(this.ItemType);
				value = set.Add(context, value);
			}
			if (value is SetValue)
			{
				HashSet<IValue> excluded = ((SetValue)value).items; 
				HashSet<IValue> values = new HashSet<IValue>();
				foreach (IValue item in items)
				{
					if (!excluded.Contains(item))
						values.Add(item);
				}
				return new SetValue(ItemType, values);
			}
			else
				throw new SyntaxError("Illegal : List - " + value.GetType().Name);
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

		public virtual IFilterable Filter(Predicate<IValue> filter)
		{
			List<IValue> list = new List<IValue>(items);
			list = list.FindAll(filter);
			HashSet<IValue> result = new HashSet<IValue>(list);
			return newInstance (result);
		}

		public override IValue GetMemberValue(Context context, String name, bool autoCreate)
		{
			if ("count" == name)
				return new IntegerValue(this.Length());
			else
				return base.GetMemberValue(context, name, autoCreate);
		}

		public IValue ToListValue()
		{
			List<IValue> list = new List<IValue>(items);
			return new ListValue(((ContainerType)type).GetItemType(), list);
		}

		public override JToken ToJsonToken()
		{
			JArray token = new JArray();
			foreach (IValue o in items)
				token.Add(o.ToJsonToken());
			return token;
		}

	}
}