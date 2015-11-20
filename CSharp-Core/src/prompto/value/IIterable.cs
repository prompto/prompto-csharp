using System.Collections.Generic;

using prompto.runtime;
using prompto.value;

namespace prompto.value
{

	public interface IIterable : IValue
	{
		bool Empty ();
		long Length ();
		IEnumerable<IValue> GetEnumerable (Context context);
	}

}

