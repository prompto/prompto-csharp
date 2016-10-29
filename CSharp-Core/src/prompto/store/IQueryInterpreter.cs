using prompto.expression;
using prompto.grammar;
using prompto.type;

namespace prompto.store
{
public interface IQueryInterpreter : IQueryFactory
	{

		IQuery buildFetchOneQuery(CategoryType type, IPredicateExpression predicate);
		IQuery buildFetchManyQuery(CategoryType type, 
			IExpression start, IExpression end, 
			IPredicateExpression predicate, 
			OrderByClauseList orderBy);

	}
}
