
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;

namespace prompto.csharp
{

	public interface CSharpExpression : IDialectElement
	{

		IType check (Context context);
		Object interpret (Context context);

	}

}