using prompto.expression;
using prompto.runtime;

namespace prompto.store
{
	public interface IPredicateExpression : IExpression
	{
		void interpretQuery(Context context, IQuery query);

	}
}
