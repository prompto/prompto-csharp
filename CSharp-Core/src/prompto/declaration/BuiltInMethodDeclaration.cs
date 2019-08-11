
using prompto.param;
using prompto.error;
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

		public BuiltInMethodDeclaration(string name, params IParameter[] parameters)
			: base(name, new ParameterList(parameters))
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
