using System;
using System.Collections.Generic;

namespace prompto.store
{
	public class NotPredicate : IPredicate
	{

		IPredicate predicate;

		public NotPredicate(IPredicate predicate)
		{
			this.predicate = predicate;
		}

		public bool matches(Dictionary<String, Object> document)
		{
			return !predicate.matches(document);
		}

	}

}
