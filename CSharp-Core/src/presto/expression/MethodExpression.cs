using System;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.type;
using presto.grammar;
using presto.utils;
using presto.value;


namespace presto.expression
{

    public class MethodExpression : IExpression
    {

        String name;

        public MethodExpression(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

		public void ToDialect(CodeWriter writer) {
			if(writer.getDialect()==Dialect.E)
				writer.append("Method: ");
			writer.append(name);
		}

        public IType check(Context context)
        {
            INamed named = context.getRegistered(name);
            if (named is MethodDeclarationMap)
                return new MethodType(context, name);
            else
                throw new SyntaxError("No method with name:" + name);
        }

        public IValue interpret(Context context)
        {
            return context.getValue(name);
        }

    }

}
