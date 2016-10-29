using System;
using prompto.declaration;

namespace prompto.store
{
	public interface IQuery
	{

		// create atomic predicates
		void verify<T>(AttributeInfo attribute, MatchOp match, T fieldValue);
		// the below make the assumption that the atomic predicates are available from a stack
		void and();
		void or();
		void not();
		// 1 based range limits
		void setFirst(long? start);
		void setLast(long? end);
		long? getFirst();
		long? getLast();
		// ordering
		void addOrderByClause(AttributeInfo attribute, bool descending);

	}
}
