using presto.error;
using presto.runtime;
using System;
using presto.parser;
using presto.expression;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.grammar
{

    public class VariableInstance : IAssignableInstance
    {

        String name;

        public VariableInstance(String i1)
        {
            this.name = i1;
        }

        public String getName()
        {
            return name;
        }

        public void ToDialect(CodeWriter writer)
        {
			writer.append(name);
        }

        public void checkAssignValue(Context context, IExpression expression)
        {
            IType type = expression.check(context);
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
                type.checkAssignableTo(context, actualType);
            }
        }

        public void checkAssignMember(Context context, String memberName)
        {
			INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual == null)
                throw new SyntaxError("Unknown variable:" + this.name);
        }

        public void checkAssignElement(Context context)
        {
            // TODO Auto-generated method stub

        }

        public void assign(Context context, IExpression expression)
        {
            IValue value = expression.interpret(context);
            if (context.getRegisteredValue<INamed>(name) == null)
				context.registerValue(new Variable(name, value.GetType(context)));
            context.setValue(name, value);
        }

        public IValue interpret(Context context)
        {
            return context.getValue(name);
        }

    }

}