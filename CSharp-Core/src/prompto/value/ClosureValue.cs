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

		public IValue interpret(Context context)
		{
			Context parentMost = this.context.getParentMostContext();
			Context savedParent = parentMost.getParentContext();
			parentMost.setParentContext(context);
			Context local = this.context.newChildContext();
			try
			{
				return doInterpret(local);
			}
			finally
			{
				parentMost.setParentContext(savedParent);
			}
		}

		private IValue doInterpret(Context local)
		{
            return Method.interpret (local);
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
