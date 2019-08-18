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

    public class ExecuteExpression : BaseExpression, IExpression, ISection
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

		public override void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				writer.append("execute: ");
				writer.append(name);
				break;
			case Dialect.O:
			case Dialect.M:
				writer.append("execute(");
				writer.append(name);
				writer.append(")");
				break;
			}
		}

        public override IType check(Context context)
        {
            try
            {
				IValue value = context.getValue(name);
				if(value is CodeValue)
					return ((CodeValue) value).check(context);
				else
					throw new SyntaxError("Expected code, got:" + value.ToString());
            }
            catch (PromptoError e)
            {
                throw new SyntaxError(e.Message);
            }
        }

		public override IValue interpret(Context context)
        {
			IValue value = context.getValue(name);
			if(value is CodeValue)
				return ((CodeValue) value).interpret(context);
			else
				throw new SyntaxError("Expected code, got:" + value.ToString());
        }
    }
}
