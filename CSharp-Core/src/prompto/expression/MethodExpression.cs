using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.value;
using prompto.declaration;
using System.Collections.Generic;

namespace prompto.expression
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
			if (context.hasValue(name))
				return context.getValue(name);
			else {
				INamed named = context.getRegistered(name);
				if (named is MethodDeclarationMap) {
					IEnumerator<IMethodDeclaration> en = ((MethodDeclarationMap)named).Values.GetEnumerator();
					en.MoveNext();
					ConcreteMethodDeclaration decl = (ConcreteMethodDeclaration)en.Current;
					return new ClosureValue(context, decl);
				} else
				throw new SyntaxError("No method with name:" + name);
			}
        }

    }

}
