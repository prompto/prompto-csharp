﻿using System.Collections.Generic;
using prompto.type;

namespace prompto.value
{
	

	/* exposes a native IValue enumerator as an IValue */
	public class IteratorValue : BaseValue, IEnumerator<IValue>
	{

		IEnumerator<IValue> source;

		public IteratorValue (IType itemType, IEnumerator<IValue> source)
			: base (new IteratorType (itemType))
		{
			this.source = source;
		}

		public bool MoveNext ()
		{
			return source.MoveNext ();
		}

		public object Current
		{
			get {
				return source.Current;
			}
		}

		IValue IEnumerator<IValue>.Current
		{
			get {
				return source.Current;
			}
		}

		public void Reset ()
		{
			source.Reset ();
		}


		public void Dispose ()
		{
			source.Dispose ();
		}

	}
}