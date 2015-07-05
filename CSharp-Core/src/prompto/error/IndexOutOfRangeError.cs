using prompto.grammar;
using prompto.runtime;
using prompto.expression;

namespace prompto.error {

public class IndexOutOfRangeError : ExecutionError {

	override
	public IExpression getExpression(Context context) {
        return context.getRegisteredValue<CategorySymbol>("INDEX_OUT_OF_RANGE");
	}
}

}