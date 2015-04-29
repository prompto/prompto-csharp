using System;
using presto.error;
using presto.runtime;
using presto.parser;
using presto.type;
using presto.grammar;
using presto.declaration;
using presto.utils;
using presto.value;

namespace presto.expression
{

    public class InstanceExpression : IExpression
    {

        String name;

        public InstanceExpression(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        override
        public String ToString()
        {
            return name;
        }

		public void ToDialect(CodeWriter writer)
		{
			ToDialect (writer, true);
		}

		public void ToDialect(CodeWriter writer, bool requireMethod)
        {
			if(requireMethod && requiresMethod(writer))
				writer.append("Method: ");
			writer.append(name);
		}

		private bool requiresMethod(CodeWriter writer) {
			if(writer.getDialect()!=Dialect.E)
				return false;
			Object o = writer.getContext().getRegistered(name);
			if(o is MethodDeclarationMap)
				return true;
			return false;
		}

        public IType check(Context context)
        {
            INamed named = context.getRegistered(name);
            if (named == null)
                throw new SyntaxError("Unknown identifier:" + name);
            else if (named is Variable) // local variable
                return named.GetType(context);
			else if(named is LinkedVariable) // local variable
				return named.GetType(context);
           else if (named is IArgument) // named argument
                return named.GetType(context);
            else if (named is CategoryDeclaration) // any p with x
                return named.GetType(context);
            else if (named is AttributeDeclaration) // in category method
                return named.GetType(context);
            else if (named is MethodDeclarationMap) // global method or closure
                return new MethodType(context, name);
            else
                throw new SyntaxError(name + "  is not an instance:" + named.GetType().Name);
        }

        public IValue interpret(Context context)
        {
            return context.getValue(name);
        }

    }

}