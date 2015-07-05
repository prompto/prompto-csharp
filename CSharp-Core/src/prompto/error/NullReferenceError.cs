using prompto.grammar;
using prompto.runtime;
using prompto.expression;

namespace prompto.error {


public class NullReferenceError : ExecutionError {

	override public IExpression getExpression(Context context) {
        return context.getRegisteredValue<CategorySymbol>("NULL_REFERENCE");
	}

}
}