using System;
using prompto.value;

namespace prompto.store
{
	public interface IStored
	{
		Object DbId { get; }
		Object GetData(String name);
	}
}
