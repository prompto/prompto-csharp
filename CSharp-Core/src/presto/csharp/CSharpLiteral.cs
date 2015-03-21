using presto.runtime;
using presto.type;
using System;
using presto.utils;


namespace presto.csharp
{

    public abstract class CSharpLiteral : CSharpExpression
    {
		String text;

		protected CSharpLiteral(String text) {
			this.text = text;
		}

		public abstract IType check(Context context);
		public abstract object interpret(Context context);

		public virtual void ToDialect(CodeWriter writer) 
		{
			writer.append(text);
		}

    }
}