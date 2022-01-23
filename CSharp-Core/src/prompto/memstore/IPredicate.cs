using System;
using System.Collections.Generic;

namespace prompto.memstore
{
	public interface IPredicate
	{
		bool matches(IDictionary<String, Object> document);
	}
}
