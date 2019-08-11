using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.declaration;
using prompto.expression;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.param;


namespace prompto.grammar
{

    public class Argument : IDialectElement
    {

        IParameter parameter;
        IExpression expression;

        public Argument(IParameter parameter, IExpression expression)
        {
            this.parameter = parameter;
            this.expression = expression;
        }

        public override string ToString()
        {
            return (parameter == null ? "" : parameter.ToString() + " ") + expression.ToString();
        }


        public void setParameter(IParameter parameter)
        {
            this.parameter = parameter;
        }


        public IParameter getParameter()
        {
            return parameter;
        }

        public String GetName()
        {
            return parameter.GetName();
        }

        public IExpression getExpression()
        {
            return expression != null ? expression : new InstanceExpression(parameter.GetName());
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public void ToDialect(CodeWriter writer)
        {
            switch (writer.getDialect())
            {
                case Dialect.E:
                    ToEDialect(writer);
                    break;
                case Dialect.O:
                    ToODialect(writer);
                    break;
                case Dialect.M:
                    toPDialect(writer);
                    break;
            }
        }

        private void ToODialect(CodeWriter writer)
        {
            if (expression == null)
                writer.append(parameter.GetName());
            else
            {
                if (parameter != null)
                {
                    writer.append(parameter.GetName());
                    writer.append(" = ");
                }
                expression.ToDialect(writer);
            }
        }


        private void toPDialect(CodeWriter writer)
        {
            if (expression == null)
                writer.append(parameter.GetName());
            else
            {
                if (parameter != null)
                {
                    writer.append(parameter.GetName());
                    writer.append(" = ");
                }
                expression.ToDialect(writer);
            }
        }

        private void ToEDialect(CodeWriter writer)
        {
            if (expression == null)
                writer.append(parameter.GetName());
            else
            {
                expression.ToDialect(writer);
                if (parameter != null)
                {
                    writer.append(" as ");
                    writer.append(parameter.GetName());
                }
            }
        }


        public override bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is Argument))
                return false;
            Argument other = (Argument)obj;
            return this.getParameter().Equals(other.getParameter())
                    && this.getExpression().Equals(other.getExpression());
        }

        public IType check(Context context)
        {
            INamed actual = context.getRegisteredValue<INamed>(parameter.GetName());
            if (actual == null)
            {
                IType actualType = getExpression().check(context);
                context.registerValue(new Variable(parameter.GetName(), actualType));
            }
            else
            {
                // need to check type compatibility
                IType actualType = actual.GetIType(context);
                IType newType = getExpression().check(context);
                actualType.checkAssignableFrom(context, newType);
            }
            return VoidType.Instance;
        }

        public IExpression resolve(Context context, IMethodDeclaration methodDeclaration, bool useInstance)
        {
            // since we support implicit members, it's time to resolve them
            String name = this.parameter.GetName();
            IExpression expression = getExpression();
            IParameter parameter = methodDeclaration.getParameters().find(name);
            IType required = parameter.GetIType(context);
            IType actual = expression.check((Context)context.getCallingContext());
            if (useInstance && actual is CategoryType)
            {
                Object value = expression.interpret((Context)context.getCallingContext());
                if (value is IInstance)
                    actual = ((IInstance)value).getType();
            }
            if (!required.isAssignableFrom(context, actual) && (actual is CategoryType))
                expression = new MemberSelector(expression, name);
            return expression;
        }

        public Argument makeArgument(Context context, IMethodDeclaration declaration)
        {
            IParameter parameter = this.parameter;
            // when 1st argument, can be unnamed
            if (parameter == null)
            {
                if (declaration.getParameters().Count == 0)
                    throw new SyntaxError("Method has no argument");
                parameter = declaration.getParameters()[0];
            }
            else
                parameter = declaration.getParameters().find(this.GetName());
            if (parameter == null)
                throw new SyntaxError("Method has no argument:" + this.GetName());
            IExpression expression = new ContextualExpression(context, this.getExpression());
            return new Argument(parameter, expression);
        }

    }

}
