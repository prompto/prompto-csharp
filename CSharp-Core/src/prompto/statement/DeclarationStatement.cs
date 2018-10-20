using prompto.error;
using prompto.runtime;
using prompto.declaration;
using prompto.type;
using prompto.value;
using prompto.utils;
using System;

namespace prompto.statement
{

	public abstract class BaseDeclarationStatement : BaseStatement
	{
		public abstract IDeclaration getDeclaration();
	}

	public class DeclarationStatement<T> : BaseDeclarationStatement where T : IDeclaration
	{

		T declaration;

		public DeclarationStatement(T declaration)
		{
			this.declaration = declaration;
		}


		public override void ToDialect(CodeWriter writer)
		{
			try
			{
				ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)(object)declaration;
				writer.getContext().registerDeclaration(method);
			}
			catch (SyntaxError /*e*/)
			{
				// ok
			}
			declaration.ToDialect(writer);
		}


		public override IDeclaration getDeclaration()
		{
			return declaration;
		}


		public override IType check(Context context)
		{
			if (declaration is ConcreteMethodDeclaration)
			{
				ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)(object)declaration;
				method.checkChild(context);
				context.registerDeclaration(method);
			}
			else
				throw new SyntaxError("Unsupported:" + declaration.GetType().Name);
			return VoidType.Instance;
		}

		public override IValue interpret(Context context)
		{
			if (declaration is ConcreteMethodDeclaration)
			{
				ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)(object)declaration;
				context.registerDeclaration(method);
				MethodType type = new MethodType(method);
				context.registerValue(new Variable(method.GetName(), type));
				context.setValue(method.GetName(), new ClosureValue(context, type));
				return null;
			}
			else
				throw new SyntaxError("Unsupported:" + declaration.GetType().Name);
		}

	}


}