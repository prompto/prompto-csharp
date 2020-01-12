using System.Collections.Generic;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.error;
using System;
using System.Collections;
using prompto.expression;
using System.Text;

namespace prompto.value
{

	public class IterableValue : BaseValue, IIterable, IFilterable, IEnumerable<IValue>, IEnumerator<IValue>
	{

		IType itemType;
		Context context;
		Integer length;
		String name;
		IEnumerator<IValue> source;
		IExpression expression;

		public IterableValue (Context context, String name, IType itemType, 
			IEnumerator<IValue> source, Integer length, IExpression expression)
			: base (new IteratorType (itemType))
		{
			this.itemType = itemType;
			this.context = context;
			this.length = length;
			this.name = name;
			this.source = source;
			this.expression = expression;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			IEnumerator<IValue> values = GetEnumerator();
			while (values.MoveNext())
			{
				if (sb.Length > 0)
					sb.Append(", ");
				sb.Append(values.Current.ToString());
			}
			return sb.ToString();
		}

		public bool Empty ()
		{
			return Length () == 0;
		}

		public long Length ()
		{
			return length.IntegerValue;
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
			return source.MoveNext ();
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
				Context child = context.newChildContext();
				child.registerValue(new Variable(name, itemType));
				child.setValue(name, source.Current);
				return expression.interpret(child);
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

		public override IValue GetMemberValue (Context context, string name, bool autoCreate)
		{
			if ("count" == name)
				return new Integer(Length());
			else
				return base.GetMemberValue(context, name, autoCreate);
		}

		public IFilterable Filter(Predicate<IValue> filter)
		{
			List<IValue> items = new List<IValue>();
			foreach (IValue item in this)
			{
				if (filter.Invoke(item))
					items.Add(item);
			}
			return new ListValue(itemType, items);
		}
	}

}
