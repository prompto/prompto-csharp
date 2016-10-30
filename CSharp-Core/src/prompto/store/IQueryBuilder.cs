using System;
using prompto.declaration;

namespace prompto.store
{
	public interface IQueryBuilder
	{

		// create atomic predicates
		void Verify<T>(AttributeInfo attribute, MatchOp match, T fieldValue);
		// the below make the assumption that the atomic predicates are available from a stack
		void And();
		void Or();
		void Not();
		// 1 based range limits
		void SetFirst(long? start);
		void SetLast(long? end);
		// ordering
		void AddOrderByClause(AttributeInfo attribute, bool descending);
		// return the built IQuery object
		IQuery Build();

	}
}
