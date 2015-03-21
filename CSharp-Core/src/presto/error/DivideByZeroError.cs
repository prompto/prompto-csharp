using presto.grammar;
using presto.runtime;
using presto.expression;

namespace presto.error {

public class DivideByZeroError : ExecutionError {

	override public IExpression getExpression(Context context) {
        return context.getRegisteredValue<CategorySymbol>("DIVIDE_BY_ZERO");
	}
}

}
