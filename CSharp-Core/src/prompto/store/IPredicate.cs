using System;
using System.Collections.Generic;

namespace prompto.store
{
	public interface IPredicate
	{
		bool matches(Dictionary<String, Object> document);
	}
}
