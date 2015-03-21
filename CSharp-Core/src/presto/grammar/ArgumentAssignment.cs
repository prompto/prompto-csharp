using System;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.declaration;
using presto.expression;
using presto.type;
using presto.value;
using presto.utils;


namespace presto.grammar {

	public class ArgumentAssignment : IDialectElement
    {

        IArgument argument;
        IExpression expression;

        public ArgumentAssignment(IArgument argument, IExpression expression)
        {
            this.argument = argument;
            this.expression = expression;
        }

		public override string ToString ()
		{
			return ( argument==null ? "" : argument.ToString () + " ") + expression.ToString ();
		}

        public IArgument getArgument()
        {
            return argument;
        }

        public String getName()
        {
            return argument.getName();
        }

        public IExpression getExpression()
        {
            return expression;
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }

		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.P:
				toPDialect(writer);
				break;
			}
		}

		private void toODialect(CodeWriter writer) {
			if(argument!=null) {
				writer.append(argument.getName());
				writer.append(" = ");
			}
			expression.ToDialect(writer);
		}

		private void toPDialect(CodeWriter writer) {
			if(argument!=null) {
				writer.append(argument.getName());
				writer.append(" = ");
			}
			expression.ToDialect(writer);
		}

		private void toEDialect(CodeWriter writer) {
			expression.ToDialect(writer);
			if(argument!=null) {
				writer.append(" as ");
				writer.append(argument.getName());
			}
		}

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is ArgumentAssignment))
                return false;
            ArgumentAssignment other = (ArgumentAssignment)obj;
            return this.getArgument().Equals(other.getArgument())
                    && this.getExpression().Equals(other.getExpression());
        }

        public IType check(Context context)
        {
			INamed actual = context.getRegisteredValue<INamed>(argument.getName());
            if (actual == null)
            {
				IType actualType = expression.check(context);
				context.registerValue(new Variable(argument.getName(), actualType));
            }
            else
            {
                // need to check type compatibility
                IType actualType = actual.GetType(context);
                IType newType = expression.check(context);
                newType.checkAssignableTo(context, actualType);
            }
            return VoidType.Instance;
        }

        public IExpression resolve(Context context, IMethodDeclaration methodDeclaration, bool checkInstance)
        {
            // since we support implicit members, it's time to resolve them
            String name = this.argument.getName();
            IExpression expression = getExpression();
            IArgument argument = methodDeclaration.getArguments().find(name);
            IType required = argument.GetType(context);
            IType actual = expression.check((Context)context.getCallingContext());
            if (checkInstance && actual is CategoryType)
            {
                Object value = expression.interpret((Context)context.getCallingContext());
                if (value is IInstance)
                    actual = ((IInstance)value).getType();
            }
            if (!actual.isAssignableTo(context, required) && (actual is CategoryType))
                expression = new MemberSelector(expression, name);
            return expression;
        }

        public ArgumentAssignment makeAssignment(Context context, IMethodDeclaration declaration)
        {
            IArgument argument = this.argument;
            // when 1st argument, can be unnamed
            if (argument == null)
            {
                if (declaration.getArguments().Count == 0)
                    throw new SyntaxError("Method has no argument");
                argument = declaration.getArguments()[0];
            }
            else
                argument = declaration.getArguments().find(this.getName());
            if (argument == null)
                throw new SyntaxError("Method has no argument:" + this.getName());
            IExpression expression = new ContextualExpression(context, this.expression);
            return new ArgumentAssignment(argument, expression);
        }

    }

}
