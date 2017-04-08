
using prompto.argument;
using prompto.error;
using prompto.grammar;
using prompto.runtime;
using prompto.value;

namespace prompto.declaration
{
	public abstract class BuiltInMethodDeclaration : BaseMethodDeclaration
	{

		public BuiltInMethodDeclaration(string name)
				: base(name, null)
		{

		}

		public BuiltInMethodDeclaration(string name, params IArgument[] arguments)
			: base(name, new ArgumentList(arguments))
		{

		}

		public override bool isAbstract()
		{
			return false;
		}
		public override bool isTemplate()
		{
			return false;
		}


		protected IValue getValue(Context context)
		{
			do
			{
				if (context is BuiltInContext)
					return ((BuiltInContext)context).getValue();
				context = context.getParentContext();
			} while (context != null);
			throw new InternalError("Could not locate context for built-in value!");
		}


		public override void ToDialect(utils.CodeWriter writer)
		{
			throw new System.NotImplementedException("Should never get there!");
		}


	}

}
