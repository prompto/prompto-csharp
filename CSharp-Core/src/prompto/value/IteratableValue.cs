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

	public class IteratableValue : BaseValue, IIterable, IEnumerable<IValue>, IEnumerator<IValue>
	{

		IType itemType;
		Context context;
		Integer length;
		String name;
		IEnumerator<IValue> source;
		IExpression expression;

		public IteratableValue (IType itemType, Context context, Integer length, String name, 
			IEnumerator<IValue> source, IExpression expression)
			: base (new IteratorType (itemType))
		{
			this.itemType = itemType;
			this.context = context;
			this.length = length;
			this.name = name;
			this.source = source;
			this.expression = expression;
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

		public override IValue GetMember (Context context, string name, bool autoCreate)
		{
			if ("length" == name)
				return new Integer (Length ());
			else
				throw new InvalidDataError ("No such member:" + name);
		}



	}

}
