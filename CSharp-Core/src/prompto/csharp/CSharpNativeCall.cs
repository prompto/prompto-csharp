using prompto.error;
using prompto.grammar;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.csharp
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