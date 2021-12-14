
using System;
using System.Collections.Generic;
using prompto.store;

namespace prompto.memstore
{
	public class Query : IQuery
	{


		Stack<IPredicate> predicates = new Stack<IPredicate>();
		List<string> projection = null;
		List<OrderBy> orderBys = new List<OrderBy>();
		long? first; // 1 based
		long? last; // 1 based

		public IPredicate GetPredicate()
		{
			if (predicates.Count==0)
				return null;
			else
				return predicates.Peek();
		}

		public void Project(List<string> projection)
        {
            this.projection = projection;
        }

		public List<string> GetProjection()
        {
			return projection;
        }

		public void SetFirst(long? first)
		{
			this.first = first;
		}

		public long? GetFirst()
		{
			return first;
		}

		public void SetLast(long? last)
		{
			this.last = last;
		}

		public long? GetLast()
		{
			return last;
		}

		public ICollection<OrderBy> GetOrdering()
		{
			if (orderBys.Count==0)
				return null;
			else
				return orderBys;
		}

		public void AddOrderByClause(AttributeInfo attribute, bool descending)
		{
			orderBys.Add(new OrderBy(attribute, descending));
		}

		public void Verify<T>(AttributeInfo info, MatchOp match, T fieldValue)
		{
			predicates.Push(new MatchesPredicate<T>(info, match, fieldValue));
		}

		public void And()
		{
			IPredicate right = predicates.Pop();
			IPredicate left = predicates.Pop();
			predicates.Push(new AndPredicate(left, right));
		}

		public void Or()
		{
			IPredicate right = predicates.Pop();
			IPredicate left = predicates.Pop();
			predicates.Push(new OrPredicate(left, right));
		}

		public void Not()
		{
			predicates.Push(new NotPredicate(predicates.Pop()));
		}



	}
}
