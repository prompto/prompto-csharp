using System;
using System.Collections.Generic;

namespace prompto.store
{
	public class AndPredicate : IPredicate
	{

		IPredicate left;
		IPredicate right;

		public AndPredicate(IPredicate left, IPredicate right)
		{
			this.left = left;
			this.right = right;
		}

		public bool matches(Dictionary<String, Object> document)
		{
			return left.matches(document) && right.matches(document);
		}

	}

}
