using prompto.parser;
using System;
using prompto.runtime;
using prompto.error;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.value;


namespace prompto.expression
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
			case Dialect.S:
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
