using presto.runtime;
using System;
using presto.grammar;
using presto.expression;

namespace presto.error {

public abstract class ExecutionError : PrestoError {

	protected ExecutionError() {
	}
	
	protected ExecutionError(String message) :
		base(message) {
	}

	public abstract IExpression getExpression(Context context);

	
}

}
