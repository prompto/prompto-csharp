
using prompto.store;

namespace prompto.memstore
{
	public class QueryBuilder : IQueryBuilder
	{
		Query query = new Query();

		public IQuery Build()
		{
			return query;
		}

		public void SetFirst(long? first)
		{
			query.SetFirst(first);
		}

		public void SetLast(long? last)
		{
			query.SetLast(last);
		}

		public void AddOrderByClause(AttributeInfo info, bool descending)
		{
			query.AddOrderByClause(info, descending);
		}

		public void Verify<T>(AttributeInfo info, MatchOp match, T fieldValue)
		{
			query.Verify(info, match, fieldValue);
		}

		public void And()
		{
			query.And();
		}

		public void Or()
		{
			query.Or();
		}

		public void Not()
		{
			query.Not();
		}

	}
}
