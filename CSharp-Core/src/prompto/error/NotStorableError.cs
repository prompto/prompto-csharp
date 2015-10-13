using prompto.grammar;
using prompto.runtime;
using prompto.expression;

namespace prompto.error
{

	public class NotStorableError : ExecutionError
	{

		public override IExpression getExpression (Context context)
		{
			return context.getRegisteredValue<CategorySymbol> ("NOT_STORABLE");
		}
	}

}
