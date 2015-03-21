
using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.grammar;
using presto.utils;

namespace presto.csharp
{

	public interface CSharpExpression : IDialectElement
	{

		IType check (Context context);
		Object interpret (Context context);

	}

}