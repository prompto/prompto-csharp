using System;
using prompto.value;
using System.Collections.Generic;
using prompto.expression;
using prompto.runtime;
using System.Collections;

namespace prompto.utils
{
	public class IValueEnumerable : IEnumerable<IValue>
	{
		Context context;
		IEnumerable<IExpression> src;

		public IValueEnumerable (Context context, IEnumerable<IExpression> src)
		{
			this.context = context;
			this.src = src;
		}

		public IEnumerator GetEnumerator()
		{
			return new IValueEnumerator(context, src.GetEnumerator());
		}

		IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
		{
			return new IValueEnumerator(context, src.GetEnumerator());
		}
	}

	public class IValueEnumerator : IEnumerator<IValue>
	{
		Context context;
		IEnumerator<IExpression> src;
		IValue current = null;

		public IValueEnumerator (Context context, IEnumerator<IExpression> src)
		{
			this.context = context;
			this.src = src;
		}
		public object Current { get { return current; } }

		IValue IEnumerator<IValue>.Current { get { return current; } }

		public bool MoveNext()
		{
			current = null;
			bool res = src.MoveNext();
			if (res)
				current = src.Current.interpret(context);
			return res;
		}

		public void Reset()
		{
			src.Reset();
			current = null;
		}

		public void Dispose()
		{
			src.Dispose();
		}

	}
}

