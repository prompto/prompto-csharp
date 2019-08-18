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

    public class MethodExpression : BaseExpression, IExpression
    {

        String name;

        public MethodExpression(String name)
        {
            this.name = name;
        }

		public override string ToString()
		{
			return name;
		}

        public String getName()
        {
            return name;
        }

		public override void ToDialect(CodeWriter writer) {
			if(writer.getDialect()==Dialect.E)
				writer.append("Method: ");
			writer.append(name);
		}

        public override IType check(Context context)
        {
            IMethodDeclaration declaration = getDeclaration(context);
            if (declaration!=null)
                return new MethodType(declaration);
            else
                throw new SyntaxError("No method with name:" + name);
        }

		private IMethodDeclaration getDeclaration(Context context)
		{
			MethodDeclarationMap methods = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
			if(methods!=null) {
				return methods.GetFirst();
			} else
				return null;
		}

        public override IValue interpret(Context context)
        {
			if (context.hasValue(name))
				return context.getValue(name);
			else {
				INamed named = context.getRegistered(name);
				if (named is MethodDeclarationMap) {
					IEnumerator<IMethodDeclaration> en = ((MethodDeclarationMap)named).Values.GetEnumerator();
					en.MoveNext();
					MethodType type = new MethodType(en.Current);
					return new ClosureValue(context, type);
				} else
				throw new SyntaxError("No method with name:" + name);
			}
        }

    }

}
