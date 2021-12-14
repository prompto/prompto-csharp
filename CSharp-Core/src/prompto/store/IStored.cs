using System;
using System.Collections.Generic;
using prompto.value;

namespace prompto.store
{
	public interface IStored
	{
		Object DbId { get; }
		bool HasData(String name);
		Object GetData(String name);
		ISet<String> Names { get; }
	}
}
