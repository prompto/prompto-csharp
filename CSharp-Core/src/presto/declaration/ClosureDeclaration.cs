using presto.grammar;
using System;
using presto.runtime;
using presto.value;


namespace presto.declaration
{

	/* a dummy declaration to interpret closures in context */
	public class ClosureDeclaration : AbstractMethodDeclaration {

		ClosureValue method;

		public ClosureDeclaration(ClosureValue method)
			: base(method.getName(),method.getArguments(),method.getReturnType())
		{
			this.method = method;
		}

		public override IValue interpret(Context context) {
			return method.interpret(context);
		}

	}
}
