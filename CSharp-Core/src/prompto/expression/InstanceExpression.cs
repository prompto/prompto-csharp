using System;
using prompto.error;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.declaration;
using prompto.utils;
using prompto.value;
using prompto.param;
using System.Collections.Generic;

namespace prompto.expression
{

    public class InstanceExpression : BaseExpression, IExpression
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

        
        public override String ToString()
        {
            return name;
        }

		public override void ToDialect(CodeWriter writer)
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

        public override IType check(Context context)
        {
            INamed named = context.getRegistered(name);
			if (named == null)
				named = context.getRegisteredDeclaration<IDeclaration>(name);
            if (named == null)
                throw new SyntaxError("Unknown identifier:" + name);
            else if (named is Variable) // local variable
                return named.GetIType(context);
			else if(named is LinkedVariable) // local variable
				return named.GetIType(context);
           else if (named is IParameter) // named argument
                return named.GetIType(context);
            else if (named is CategoryDeclaration) // any p with x
                return named.GetIType(context);
            else if (named is AttributeDeclaration) // in category method
                return named.GetIType(context);
			else if (named is MethodDeclarationMap) { // global method or closure
				IEnumerator<IMethodDeclaration> decls = ((MethodDeclarationMap)named).Values.GetEnumerator();
				decls.MoveNext();
				return new MethodType(decls.Current);
			} else
                throw new SyntaxError(name + "  is not a value or method:" + named.GetType().Name);
        }

        public override IValue interpret(Context context)
        {
    		if (context.hasValue(name))
				return context.getValue(name);
			else {
				INamed named = context.getRegistered(name);
				if (named is MethodDeclarationMap)
				{
					IMethodDeclaration decl = ((MethodDeclarationMap)named).GetFirst();
					return new ClosureValue(context, new MethodType(decl));
				}
				else
					throw new SyntaxError("No value or method with name:" + name);
			}
    }

    }

}