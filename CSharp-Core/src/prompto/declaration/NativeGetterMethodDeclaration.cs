using System;
using prompto.csharp;
using prompto.value;
using prompto.runtime;
using prompto.error;
using prompto.statement;

namespace prompto.declaration
{
	public class NativeGetterMethodDeclaration : GetterMethodDeclaration
	{

		CSharpNativeCall statement;

		public NativeGetterMethodDeclaration (String name, StatementList statements)
			: base (name, statements)
		{
			statement = findNativeStatement ();
		}

		private CSharpNativeCall findNativeStatement ()
		{
			foreach (IStatement statement in statements) {
				if (statement is CSharpNativeCall)
					return (CSharpNativeCall)statement;
			}
			return null;
		}

		public override IValue interpret (Context context)
		{
			context.enterMethod (this);
			try {
				return doInterpretNative (context);
			} finally {
				context.leaveMethod (this);
			}
		}

		private IValue doInterpretNative (Context context)
		{
			context.enterStatement (statement);
			try {
				IValue result = statement.interpretNative (context, returnType);
				if (result != null)
					return result;
			} finally {
				context.leaveStatement (statement);
			}
			return null;
		}

		public override type.IType checkChild(Context context)
		{
			return check(context);
		}

	}
}
