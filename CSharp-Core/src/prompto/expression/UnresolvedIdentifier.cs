

using prompto.runtime;
using System;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.statement;
using prompto.declaration;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{

    public class UnresolvedIdentifier : IExpression
    {

		Dialect dialect;
		String name;
        IExpression resolved;

        public UnresolvedIdentifier(String name, Dialect dialect)
        {
            this.name = name;
			this.dialect = dialect;
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
			} catch(SyntaxError /*e*/) {
			}
			if(resolved!=null)
				resolved.ToDialect(writer);
			else
				writer.append(name);
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
				resolved = doResolve(context, forMember);
			}
            if (resolved != null)
				return resolved;
			else
                throw new SyntaxError("Unknown identifier:" + name);
        }

		IExpression doResolve(Context context, bool forMember)
		{
			IExpression resolved = resolveSymbol(context);
			if (resolved != null)
				return resolved;
			resolved = resolveTypeOrConstructor(context, forMember);
			if (resolved != null)
				return resolved;
			resolved = resolveMethodCall(context);
			if (resolved != null)
				return resolved;
			resolved = resolveInstance(context);
			return resolved;
		}

		IExpression resolveTypeOrConstructor(Context context, bool forMember)
		{
			if (!Char.IsUpper(name[0]))
				return null;
			if (forMember)
				return resolveType(context);
			else
				return resolveConstructor(context);
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

        private IExpression resolveMethodCall(Context context)
        {
			if (dialect != Dialect.E)
				return null;
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
				IExpression method = new ConstructorExpression(new CategoryType(name), null, null, true);
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
			if (decl is EnumeratedCategoryDeclaration)
				return new TypeExpression(new EnumeratedCategoryType(name));
            else if (decl is CategoryDeclaration)
                return new TypeExpression(new CategoryType(name));
            else if (decl is EnumeratedNativeDeclaration)
                return new TypeExpression(decl.GetIType(context));
            else foreach (IType type in NativeType.getAll())
             {
				if (name == type.GetTypeName())
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
