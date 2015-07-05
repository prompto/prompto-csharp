using System.Collections.Generic;
using prompto.runtime;
using System;
using prompto.error;
using prompto.type;
using prompto.utils;
using prompto.parser;
using prompto.value;
using prompto.csharp;


namespace prompto.statement
{

    public class StatementList : List<IStatement>
    {

        public StatementList()
        {
        }

        public StatementList(IStatement statement)
        {
            this.Add(statement);
        }

        /* for unified grammar */ 
        public void add(IStatement statement)
        {
            this.Add(statement);
        }
        
        public IType check(Context context)
        {
		TypeMap types = new TypeMap();
		foreach(IStatement statement in this) {
			IType type = statement.check(context);
			if(type!=VoidType.Instance)
					types[type.GetName()] = type;
		}
		return types.inferType(context);
	}

		public IValue interpret(Context context)
        {
            try
            {
                return doInterpret(context);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceError();
            }
        }

		private IValue doInterpret(Context context)
        {
            foreach (IStatement statement in this)
            {
                context.enterStatement(statement);
                try
                {
					IValue result = statement.interpret(context);
                    if (result != null)
                        return result;
                }
                finally
                {
                    context.leaveStatement(statement);
                }
            }
            return null;
        }

		public IValue interpretNative(Context context, IType returnType)
		{
			try
			{
				return doInterpretNative(context, returnType);
			}
			catch (NullReferenceException)
			{
				throw new NullReferenceError();
			}
		}

		private IValue doInterpretNative(Context context, IType returnType)
		{
			foreach (IStatement statement in this)
			{
				if(!(statement is CSharpNativeCall))
					continue;
				context.enterStatement(statement);
				try
				{
					IValue result = ((CSharpNativeCall)statement).interpretNative(context, returnType);
					if (result != null)
						return result;
				}
				finally
				{
					context.leaveStatement(statement);
				}
			}
			return null;
		}

		public void ToDialect(CodeWriter writer) {
			foreach(IStatement statement in this) {
				statement.ToDialect(writer);
				if(statement is SimpleStatement) {
					if(writer.getDialect()==Dialect.O)
						writer.append(';');
					writer.newLine();
				}
			}
		}
	}
}
