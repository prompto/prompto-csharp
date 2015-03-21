using presto.grammar;
using presto.runtime;
using presto.expression;

namespace presto.error {


public class NullReferenceError : ExecutionError {

	override public IExpression getExpression(Context context) {
        return context.getRegisteredValue<CategorySymbol>("NULL_REFERENCE");
	}

}
}