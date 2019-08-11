using prompto.declaration;
using prompto.runtime;
using prompto.type;
using System;
using prompto.param;

namespace prompto.value
{


	public class ClosureValue : BaseValue
	{

		Context context;

		public ClosureValue (Context context, MethodType type)
			: base(type)
		{
			this.context = context;
		}

		public IValue interpret (Context context)
		{
			Context parentMost = this.context.getParentMostContext ();
			parentMost.setParentContext (context);
			IValue result = Method.interpret (this.context);
			parentMost.setParentContext (null);
			return result;
		}

		private IMethodDeclaration Method
		{
			get
			{
				return ((MethodType)type).Method;
			}
		}

		public String getName ()
		{
			return Method.GetName ();
		}

		public ParameterList getParameters ()
		{
			return Method.getParameters ();
		}

		public IType getReturnType ()
		{
			return Method.getReturnType ();
		}

	}
}
