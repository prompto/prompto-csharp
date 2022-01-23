using System;
using System.Collections.Generic;

namespace prompto.memstore
{
	public class NotPredicate : IPredicate
	{

		IPredicate predicate;

		public NotPredicate(IPredicate predicate)
		{
			this.predicate = predicate;
		}

		public bool matches(IDictionary<String, Object> document)
		{
			return !predicate.matches(document);
		}

	}

}
