
using System.Collections.Generic;
using System.Collections.ObjectModel;
using prompto.declaration;

namespace prompto.store
{
	public class Query : IQuery
	{


		Stack<IPredicate> predicates = new Stack<IPredicate>();
		List<IOrderBy> orderBys = new List<IOrderBy>();
		long? first; // 1 based
		long? last; // 1 based

		public IPredicate GetPredicate()
		{
			if (predicates.Count==0)
				return null;
			else
				return predicates.Peek();
		}

		public void setFirst(long? first)
		{
			this.first = first;
		}

		public long? getFirst()
		{
			return first;
		}

		public void setLast(long? last)
		{
			this.last = last;
		}

		public long? getLast()
		{
			return last;
		}

		public ICollection<IOrderBy> getOrdering()
		{
			if (orderBys.Count==0)
				return null;
			else
				return orderBys;
		}

		public void addOrderByClause(AttributeInfo attribute, bool descending)
		{
			orderBys.Add(new OrderBy(attribute, descending));
		}

		public void verify<T>(AttributeInfo info, MatchOp match, T fieldValue)
		{
			predicates.Push(new MatchesPredicate<T>(info, match, fieldValue));
		}

		public void and()
		{
			IPredicate right = predicates.Pop();
			IPredicate left = predicates.Pop();
			predicates.Push(new AndPredicate(left, right));
		}

		public void or()
		{
			IPredicate right = predicates.Pop();
			IPredicate left = predicates.Pop();
			predicates.Push(new OrPredicate(left, right));
		}

		public void not()
		{
			predicates.Push(new NotPredicate(predicates.Pop()));
		}



	}
}
