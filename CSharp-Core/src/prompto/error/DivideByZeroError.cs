using prompto.grammar;
using prompto.runtime;
using prompto.expression;

namespace prompto.error {

public class DivideByZeroError : ExecutionError {

	override public IExpression getExpression(Context context) {
        return context.getRegisteredValue<CategorySymbol>("DIVIDE_BY_ZERO");
	}
}

}
