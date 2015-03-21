using presto.error;
using presto.runtime;
using System;
using presto.parser;
using presto.grammar;
using presto.expression;
using presto.type;
using presto.declaration;
using presto.value;
using presto.utils;


namespace presto.statement
{

	public class AssignVariableStatement : SimpleStatement
    {

        String name;
        IExpression expression;

		public AssignVariableStatement(String name, IExpression expression)
        {
			this.name = name;
            this.expression = expression;
        }

        public String getName()
        {
			return name;
        }

        public IExpression getExpression()
        {
            return expression;
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }

        override
		public void ToDialect(CodeWriter writer)
        {
			writer.append(name);
			writer.append(" = ");
			expression.ToDialect(writer);
        }

        public IType checkResource(Context context)
        {
            IType type = expression.check(context);
            if (!(type is ResourceType))
                throw new SyntaxError("Not a resource!");
			INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual == null)
				context.registerValue(new Variable(name, type));
            else
            {
                // need to check type compatibility
                IType actualType = actual.GetType(context);
                type.checkAssignableTo(context, actualType);
            }
            return VoidType.Instance;
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is AssignVariableStatement))
                return false;
            AssignVariableStatement other = (AssignVariableStatement)obj;
			return this.name.Equals(other.name)
                    && this.getExpression().Equals(other.getExpression());
        }

        override
        public IType check(Context context)
        {
			INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual == null)
            {
				IType actualType = expression.check(context);
				context.registerValue(new Variable(name, actualType));
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

        override
        public IValue interpret(Context context)
        {
			INamed named = context.getRegisteredValue<INamed> (name);
			if (named == null) {
				IType type = expression.check (context);
				context.registerValue (new Variable (name, type));
			}
			context.setValue(name, expression.interpret(context));
            return null;
        }

        public IExpression resolve(Context context, IMethodDeclaration methodDeclaration, bool checkInstance)
        {
            // since we support implicit members, it's time to resolve them
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

    }

}