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

 
        
		public override void ToDialect(CodeWriter writer)
        {
			writer.append("C#: ");
			statement.ToDialect(writer);
        }

		public override IType check(Context context)
		{
			throw new Exception ("Should never get there!");
		}
        
		public IType checkNative(Context context, IType returnType)
        {
			return statement.check(context, returnType);
         }

        public override IValue interpret(Context context)
		{
			throw new Exception ("Should never get there!");
		}

		public IValue interpretNative(Context context, IType returnType)
		{
			return statement.interpret(context, returnType);
        }

    }

}