using prompto.runtime;
using prompto.store;

namespace prompto.expression
{
	public interface IPredicateExpression : IExpression
	{
		void interpretQuery(Context context, IQueryBuilder builder);

	}
}
