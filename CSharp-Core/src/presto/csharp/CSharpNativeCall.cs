using presto.error;
using presto.grammar;
using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.csharp
{

    public class CSharpNativeCall : NativeCall
    {

        CSharpStatement statement;

        public CSharpNativeCall(CSharpStatement statement)
        {
            this.statement = statement;
        }

 
        override
		public void ToDialect(CodeWriter writer)
        {
			writer.append("C#: ");
			statement.ToDialect(writer);
        }

        override
        public IType check(Context context)
        {
            return statement.check(context);
         }

        override
		public IValue interpret(Context context)
		{
			throw new Exception ("Should never get there!");
		}

		public IValue interpretNative(Context context, IType returnType)
		{
			return statement.interpret(context, returnType);
        }

    }

}