using System.Collections.Generic;
using presto.runtime;
using System;
using presto.error;
using presto.type;
using presto.utils;
using presto.parser;
using presto.value;


namespace presto.statement
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
				types[type.getName()] = type;
		}
		return types.inferType(context);
	}

		public IValue interpret(Context context)
        {
            try
            {
                return doEvaluate(context);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceError();
            }
        }

		private IValue doEvaluate(Context context)
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
