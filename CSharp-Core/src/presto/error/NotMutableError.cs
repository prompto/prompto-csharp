using presto.grammar;
using presto.runtime;
using presto.expression;

namespace presto.error
{

	public class NotMutableError : ExecutionError
	{

		public override IExpression getExpression (Context context)
		{
			return context.getRegisteredValue<CategorySymbol> ("NOT_MUTABLE");
		}
	}

}
