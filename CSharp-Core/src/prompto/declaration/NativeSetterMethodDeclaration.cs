using System;
using prompto.statement;

namespace prompto.declaration
{
	public class NativeSetterMethodDeclaration : SetterMethodDeclaration {

		public NativeSetterMethodDeclaration(String name, StatementList statements)
			: base(name, statements)
		{
		}

		public override type.IType checkChild(runtime.Context context)
		{
			return check(context);
		}

	}

}

