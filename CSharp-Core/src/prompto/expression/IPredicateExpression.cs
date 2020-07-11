using prompto.runtime;
using prompto.store;
using prompto.type;

namespace prompto.expression
{
	public interface IPredicateExpression : IExpression
	{
		void checkQuery(Context context);
		void interpretQuery(Context context, IQueryBuilder builder);

	}
}
