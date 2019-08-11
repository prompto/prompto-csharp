using prompto.runtime;
using System;
using prompto.grammar;
using prompto.expression;
using prompto.value;
using prompto.literal;
using prompto.type;
using prompto.param;

namespace prompto.error {

public abstract class ExecutionError : PromptoError {

	protected ExecutionError() {
	}
	
	protected ExecutionError(String message) :
		base(message) {
	}

	public abstract IExpression getExpression(Context context);

	public IValue interpret(Context context, String errorName) {
			IExpression exp = this.getExpression(context);
			if(exp==null) {
				ArgumentList args = new ArgumentList();
				args.Add(new Argument(new UnresolvedParameter("name"), new TextLiteral(this.GetType().Name)));
				args.Add(new Argument(new UnresolvedParameter("text"), new TextLiteral(this.Message)));
				exp = new ConstructorExpression(new CategoryType("Error"), null, args, true);
			}
			if(context.getRegisteredValue<INamed>(errorName)==null)
				context.registerValue(new ErrorVariable(errorName));
			IValue error = exp.interpret(context);
			context.setValue(errorName, error);
			return error;
		}

}

}
