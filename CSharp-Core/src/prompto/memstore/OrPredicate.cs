using System;
using System.Collections.Generic;

namespace prompto.memstore
{
	public class OrPredicate : IPredicate
	{

		IPredicate left;
		IPredicate right;

		public OrPredicate(IPredicate left, IPredicate right)
		{
			this.left = left;
			this.right = right;
		}

		public bool matches(IDictionary<String, Object> document)
		{
			return left.matches(document) || right.matches(document);
		}

	}

}
