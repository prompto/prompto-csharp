using prompto.runtime;
using prompto.type;
using System;
using prompto.utils;


namespace prompto.csharp
{

    public abstract class CSharpLiteral : CSharpExpression
    {
		String text;

		protected CSharpLiteral(String text) {
			this.text = text;
		}

		public override void ToDialect(CodeWriter writer) 
		{
			writer.append(text);
		}

    }
}