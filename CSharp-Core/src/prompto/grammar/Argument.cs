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

        public IParameter Parameter { get; set; }
        public IExpression Expression { get; set; }

        public Argument(IParameter parameter, IExpression expression)
        {
            this.Parameter = parameter;
            this.Expression = expression;
        }

        public override string ToString()
        {
            return (Parameter == null ? "" : Parameter.ToString() + " ") + Expression.ToString();
        }


        public String GetName()
        {
            return Parameter.GetName();
        }

        public IExpression getExpression()
        {
            return Expression != null ? Expression : new InstanceExpression(Parameter.GetName());
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
            if (Expression == null)
                writer.append(Parameter.GetName());
            else
            {
                if (Parameter != null)
                {
                    writer.append(Parameter.GetName());
                    writer.append(" = ");
                }
                Expression.ToDialect(writer);
            }
        }


        private void toPDialect(CodeWriter writer)
        {
            if (Expression == null)
                writer.append(Parameter.GetName());
            else
            {
                if (Parameter != null)
                {
                    writer.append(Parameter.GetName());
                    writer.append(" = ");
                }
                Expression.ToDialect(writer);
            }
        }

        private void ToEDialect(CodeWriter writer)
        {
            if (Expression == null)
                writer.append(Parameter.GetName());
            else
            {
                Expression.ToDialect(writer);
                if (Parameter != null)
                {
                    writer.append(" as ");
                    writer.append(Parameter.GetName());
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
            return this.Parameter.Equals(other.Parameter)
                    && this.getExpression().Equals(other.getExpression());
        }

        public IType check(Context context)
        {
            INamed actual = context.getRegisteredValue<INamed>(Parameter.GetName());
            if (actual == null)
            {
                IType actualType = getExpression().check(context);
                context.registerValue(new Variable(Parameter.GetName(), actualType));
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
            String name = this.Parameter.GetName();
            IExpression exp = getExpression();
            IParameter param = methodDeclaration.getParameters().find(name);
            IType requiredType = param.GetIType(context);
            bool checkArrow = requiredType is MethodType && exp is ContextualExpression && ((ContextualExpression)exp).Expression is ArrowExpression;
            IType actualType = checkArrow ? ((MethodType)requiredType).checkArrowExpression((ContextualExpression)exp) : exp.check(context.getCallingContext());
            if (useInstance && actualType is CategoryType)
            {
                Object value = exp.interpret((Context)context.getCallingContext());
                if (value is IInstance)
                    actualType = ((IInstance)value).getType();
            }
            if (!requiredType.isAssignableFrom(context, actualType) && (actualType is CategoryType))
                exp = new MemberSelector(exp, name);
            return exp;
        }

        public Argument makeArgument(Context context, IMethodDeclaration declaration)
        {
            IParameter parameter = Parameter;
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
