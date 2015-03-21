using presto.parser;
using System;
using presto.runtime;
using presto.error;
using presto.type;
using presto.grammar;
using presto.utils;
using presto.value;


namespace presto.expression
{

    public class ExecuteExpression : Section, IExpression, ISection
    {

        String name;

        public ExecuteExpression(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				writer.append("execute: ");
				writer.append(name);
				break;
			case Dialect.O:
			case Dialect.P:
				writer.append("execute(");
				writer.append(name);
				writer.append(")");
				break;
			}
		}

        public IType check(Context context)
        {
            try
            {
				IValue value = context.getValue(name);
				if(value is CodeValue)
					return ((CodeValue) value).check(context);
				else
					throw new SyntaxError("Expected code, got:" + value.ToString());
            }
            catch (PrestoError e)
            {
                throw new SyntaxError(e.Message);
            }
        }

		public IValue interpret(Context context)
        {
			IValue value = context.getValue(name);
			if(value is CodeValue)
				return ((CodeValue) value).interpret(context);
			else
				throw new SyntaxError("Expected code, got:" + value.ToString());
        }
    }
}
