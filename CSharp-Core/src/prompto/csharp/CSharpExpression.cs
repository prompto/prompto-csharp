
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;

namespace prompto.csharp
{

	public abstract class CSharpExpression : IDialectElement
	{
        public abstract IType check (Context context);
        public abstract Object interpret (Context context);
        public abstract void ToDialect(CodeWriter writer);
        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }

    }

}