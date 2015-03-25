using presto.runtime;
using System;
using presto.grammar;
using presto.expression;
using presto.value;
using presto.literal;
using presto.type;

namespace presto.error {

public abstract class ExecutionError : PrestoError {

	protected ExecutionError() {
	}
	
	protected ExecutionError(String message) :
		base(message) {
	}

	public abstract IExpression getExpression(Context context);

	public IValue interpret(Context context, String errorName) {
			IExpression exp = this.getExpression(context);
			if(exp==null) {
				ArgumentAssignmentList args = new ArgumentAssignmentList();
				args.add(new ArgumentAssignment(new UnresolvedArgument("name"), new TextLiteral(this.GetType().Name)));
				args.add(new ArgumentAssignment(new UnresolvedArgument("text"), new TextLiteral(this.Message)));
				exp = new ConstructorExpression(new CategoryType("Error"), args);
			}
			if(context.getRegisteredValue<INamed>(errorName)==null)
				context.registerValue(new ErrorVariable(errorName));
			IValue error = exp.interpret(context);
			context.setValue(errorName, error);
			return error;
		}

}

}
