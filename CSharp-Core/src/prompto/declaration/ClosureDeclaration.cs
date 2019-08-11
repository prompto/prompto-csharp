using prompto.runtime;
using prompto.value;


namespace prompto.declaration
{

	/* a dummy declaration to interpret closures in context */
	public class ClosureDeclaration : AbstractMethodDeclaration {

		ClosureValue method;

		public ClosureDeclaration(ClosureValue method)
			: base(method.getName(),method.getParameters(),method.getReturnType())
		{
			this.method = method;
		}

		public override IValue interpret(Context context) {
			return method.interpret(context);
		}

	}
}
