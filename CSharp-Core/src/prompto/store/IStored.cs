using System;
using System.Collections.Generic;
using prompto.value;

namespace prompto.store
{
	public interface IStored
	{
		Object DbId { get; }
		Object GetData(String name);
		ISet<String> Names { get; }
	}
}
