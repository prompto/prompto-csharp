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
                    ToMDialect(writer);
                    break;
            }
        }

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
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


        private void ToMDialect(CodeWriter writer)
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

        public IExpression resolve(Context context, IMethodDeclaration methodDeclaration, bool checkInstance)
        {
            IParameter parameter = findParameter(methodDeclaration);
            return resolve(context, parameter, checkInstance);
        }

        private IParameter findParameter(IMethodDeclaration methodDeclaration)
        {
            String name = this.Parameter.GetName();
            return methodDeclaration.getParameters().find(name);
        }

        public IExpression resolve(Context context, IParameter parameter, bool checkInstance)
        {
            // since we support implicit members, it's time to resolve them
            IExpression expression = getExpression();
            IType requiredType = parameter.GetIType(context);
            IType actualType = checkActualType(context, requiredType, expression, checkInstance);
            bool assignable = requiredType.isAssignableFrom(context, actualType);
            // try passing category member
            if (!assignable && (actualType is CategoryType)) 
			    expression = new MemberSelector(expression, parameter.GetName());
            return expression;
       }

        public IType checkActualType(Context context, IType requiredType, IExpression expression, bool checkInstance)
        {
            IType actualType = null;
            bool isArrow = isArrowExpression(expression);
            if (isArrow)
            {
                if (requiredType is MethodType)
                    actualType = checkArrowExpression(context, (MethodType)requiredType, expression);
                else
                    actualType = VoidType.Instance;
            }
            else if(requiredType is MethodType)
                actualType = expression.checkReference(context.getCallingContext());
            else
                actualType = expression.check(context.getCallingContext());
            if (checkInstance && actualType is CategoryType) {
                Object value = expression.interpret(context.getCallingContext());
                if (value is IInstance)
				    actualType = ((IInstance)value).getType();
            }
            return actualType;
        }

        private IType checkArrowExpression(Context context, MethodType requiredType, IExpression expression)
        {
            context = expression is ContextualExpression ? ((ContextualExpression)expression).Calling : context.getCallingContext();
            ArrowExpression arrow = (ArrowExpression)(expression is ArrowExpression ? expression: ((ContextualExpression)expression).Expression);
            return requiredType.checkArrowExpression(context, arrow);
        }


        private bool isArrowExpression(IExpression expression)
        {
            if (expression is ContextualExpression)
                expression = ((ContextualExpression)expression).Expression;
            return expression is ArrowExpression;
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
