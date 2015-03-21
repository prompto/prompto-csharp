using presto.declaration;
using presto.runtime;
using presto.type;
using System;
using presto.grammar;

namespace presto.value
{


	public class ClosureValue : BaseValue
	{

		IMethodDeclaration method;

		public ClosureValue (Context context, IMethodDeclaration method)
			: base(new MethodType (context, method.getName ()))
		{
			this.method = method;
		}

		public IMethodDeclaration getMethodDeclaration ()
		{
			return method;
		}

		public IValue interpret (Context context)
		{
			Context thisContext = ((MethodType)type).GetContext ();
			Context parentMost = thisContext.getParentMostContext ();
			parentMost.setParentContext (context);
			IValue result = method.interpret (thisContext);
			parentMost.setParentContext (null);
			return result;
		}

		public String getName ()
		{
			return method.getName ();
		}

		public ArgumentList getArguments ()
		{
			return method.getArguments ();
		}

		public IType getReturnType ()
		{
			return method.getReturnType ();
		}

	}
}
