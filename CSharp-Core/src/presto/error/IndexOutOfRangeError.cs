using presto.grammar;
using presto.runtime;
using presto.expression;

namespace presto.error {

public class IndexOutOfRangeError : ExecutionError {

	override
	public IExpression getExpression(Context context) {
        return context.getRegisteredValue<CategorySymbol>("INDEX_OUT_OF_RANGE");
	}
}

}