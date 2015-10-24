using System.Collections.Generic;

using prompto.runtime;
using prompto.value;

namespace prompto.store
{

	public interface ICursor<T> : IValue where T : IValue
	{
		bool Empty ();
		long Length ();
		IEnumerable<T> GetItems (Context context);
	}

}

