

using presto.runtime;
using System;
using presto.error;
using presto.parser;
using presto.type;
using presto.expression;
using presto.statement;
using presto.declaration;
using presto.utils;
using presto.value;

namespace presto.grammar
{

    public class UnresolvedIdentifier : IExpression
    {

        String name;
        IExpression resolved;

        public UnresolvedIdentifier(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        public IExpression getResolved()
        {
            return resolved;
        }

        override
        public String ToString()
        {
            return name;
        }

        public void ToDialect(CodeWriter writer)
        {
			try {
				resolve(writer.getContext(), false);
				resolved.ToDialect(writer);
			} catch(SyntaxError /*e*/) {
				writer.append(name);
			}
        }


        public IType check(Context context)
        {
            return resolveAndCheck(context, false);
        }

        public IType checkMember(Context context)
        {
            return resolveAndCheck(context, true);
        }

		public IValue interpret(Context context)
        {
            if (resolved == null)
                resolveAndCheck(context, false);
            return resolved.interpret(context);
        }

        private IType resolveAndCheck(Context context, bool forMember)
		{
			resolve(context, forMember);
			return resolved.check(context);
		}

		public IExpression resolve(Context context, bool forMember)
		{
			if (resolved == null) {
				resolved = resolveSymbol(context);
				if (resolved == null) {
					if(Char.IsUpper(name[0]))
		            {
		                if (forMember)
		                    resolved = resolveType(context);
		                else
		                    resolved = resolveConstructor(context);
		            }
					if (resolved == null) {
                		resolved = resolveMethod(context);
			            if (resolved == null)
			                resolved = resolveInstance(context);
					}
				}
			}
            if (resolved != null)
				return resolved;
			else
                throw new SyntaxError("Unknown identifier:" + name);
        }

        private IExpression resolveInstance(Context context)
        {
            try
            {
                IExpression id = new InstanceExpression(name);
                id.check(context);
                return id;
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        private IExpression resolveMethod(Context context)
        {
            try
            {
                IExpression method = new MethodCall(new MethodSelector(name));
                method.check(context);
                return method;
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        private IExpression resolveConstructor(Context context)
        {
            try
            {
                IExpression method = new ConstructorExpression(new CategoryType(name), false, null);
                method.check(context);
                return method;
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        private IExpression resolveType(Context context)
        {
            IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(name);
            if (decl is CategoryDeclaration)
                return new TypeExpression(new CategoryType(name));
            else if (decl is EnumeratedNativeDeclaration)
                return new TypeExpression(decl.GetType(context));
            else foreach (IType type in NativeType.getAll())
             {
				if (name == type.GetName())
                        return new TypeExpression(type);
                }
            return null;
        }

        private IExpression resolveSymbol(Context context)
        {
            if (name == name.ToUpper())
                return new SymbolExpression(name);
            else
                return null;
        }

    }

}
