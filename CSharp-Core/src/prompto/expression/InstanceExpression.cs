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
using prompto.store;
using prompto.literal;

namespace prompto.expression
{

    public class InstanceExpression : BaseExpression, IPredicateExpression
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
            if (named is Variable) // local variable
                return named.GetIType(context);
            else if (named is LinkedVariable) // local variable
                return named.GetIType(context);
            else if (named is IParameter) // named argument
                return named.GetIType(context);
            else if (named is CategoryDeclaration) // any p with x
                return named.GetIType(context);
            else if (named is AttributeDeclaration) // in category method
                return named.GetIType(context);
            else if (named is MethodDeclarationMap)
            { // global method or closure
                IEnumerator<IMethodDeclaration> decls = ((MethodDeclarationMap)named).Values.GetEnumerator();
                decls.MoveNext();
                return new MethodType(decls.Current);
            }
            else if (named != null)
                throw new SyntaxError(name + "  is not a value or method:" + named.GetType().Name);
            else
                throw new SyntaxError("Unknown identifier " + name);
        }

        public override AttributeDeclaration CheckAttribute(Context context)
        {
            AttributeDeclaration decl = context.findAttribute(this.name);
            return decl != null ? decl : base.CheckAttribute(context);
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

        public void checkQuery(Context context)
        {
            IPredicateExpression predicate = ToPredicate(context);
            if (predicate != null)
                predicate.checkQuery(context);
            else
                throw new SyntaxError("Expected a predicate, found: " + name);
        }


        private IPredicateExpression ToPredicate(Context context)
        {
            AttributeDeclaration decl = context.findAttribute(name);
            if (decl == null)
                throw new SyntaxError("Unknown identifier:" + name);
            else if (decl.GetIType(context) != BooleanType.Instance)
                throw new SyntaxError("Expected a Boolean, found:" + decl.GetIType(context).GetTypeName());
            else
                return new EqualsExpression(this, EqOp.EQUALS, new BooleanLiteral("true"));
        }

        public void interpretQuery(Context context, IQueryBuilder builder)
        {
            IPredicateExpression predicate = ToPredicate(context);
            if(predicate!=null)
                predicate.interpretQuery(context, builder);
        }

     }

}