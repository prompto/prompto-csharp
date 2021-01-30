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
using prompto.literal;

namespace prompto.expression
{

    public class MethodExpression : BaseExpression, IExpression
    {

		IExpression expression;

        public MethodExpression(IExpression expression)
        {
            this.expression = expression;
        }

		public override string ToString()
		{
			return expression.ToString();
		}

        public IExpression getExpression()
        {
            return expression;
        }

		public TypeLiteral AsTypeLiteral(Context context)
		{
			if (expression is UnresolvedIdentifier) {
				String name = ((UnresolvedIdentifier)expression).getName();
				return new TypeLiteral(new CategoryType(name));
			} else
				throw new NotSupportedException();
		}

		public override void ToDialect(CodeWriter writer) {
			if(writer.getDialect()==Dialect.E)
				writer.append("Method: ");
			if (expression is UnresolvedIdentifier)
			{
				writer.append(expression.ToString());
			}
			else if (expression is UnresolvedSelector)
			{
				writer.append(expression.ToString());
			}
			else
				throw new NotImplementedException();
		}

        public override IType check(Context context)
        {
			IMethodDeclaration declaration = getDeclaration(context);
			if (declaration!=null)
				return new MethodType(declaration);
			else
				throw new SyntaxError("Not a method:" + expression.ToString());
        }

		private IMethodDeclaration getDeclaration(Context context)
		{
			IExpression expression = this.expression;
			if (expression is UnresolvedSelector) {
				IExpression parent = ((UnresolvedSelector)expression).getParent();
				if (parent != null)
				{
					IType type = parent.check(context);
					if (type is CategoryType) {
						expression = new UnresolvedIdentifier(((UnresolvedSelector)expression).getName(), Dialect.O);
						context = context.newInstanceContext((CategoryType)type, true);
					} else
						return null; // TODO report problem
				}
			}
			if (expression is UnresolvedIdentifier) {
				String name = ((UnresolvedIdentifier)expression).ToString();
				MethodDeclarationMap methods = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
				if(methods!=null) {
					return methods.GetFirst();
				} else
					return null;
			}
			else
				throw new NotImplementedException();
		}

        public override IValue interpret(Context context)
        {
			IExpression expression = this.expression;
			if (expression is UnresolvedSelector) {
				IExpression parent = ((UnresolvedSelector)expression).getParent();
				if (parent != null)
				{
					IValue value = parent.interpret(context);
					if (value is IInstance) {
						expression = new UnresolvedIdentifier(((UnresolvedSelector)expression).getName(), Dialect.O);
						context = context.newInstanceContext((IInstance)value, true);
					} else
						return NullValue.Instance; // TODO throw error
				}
			}
			if (expression is UnresolvedIdentifier)
			{
				String name = ((UnresolvedIdentifier)expression).ToString();
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
			else
				throw new NotImplementedException();
		}

     }

}
