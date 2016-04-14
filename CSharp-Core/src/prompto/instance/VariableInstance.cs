using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.expression;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.grammar;

namespace prompto.instance
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

		public void ToDialect(CodeWriter writer, IExpression expression)
        {
			if(expression!=null) try {
				IType type = expression.check(writer.getContext());
				INamed actual = writer.getContext().getRegisteredValue<INamed>(name);
				if(actual==null)
					writer.getContext().registerValue(new Variable(name, type));
			} catch(SyntaxError) {
				// TODO warning
			}
			writer.append(name);
        }

        public void checkAssignValue(Context context, IExpression expression)
        {
            IType type = expression.check(context);
            INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual == null)
            {
				IType actualType = type;
				context.registerValue(new Variable(name, actualType));
            }
            else
            {
                // need to check type compatibility
                IType actualType = actual.GetIType(context);
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
				context.registerValue(new Variable(name, value.GetIType()));
            context.setValue(name, value);
        }

        public IValue interpret(Context context)
        {
            return context.getValue(name);
        }

    }

}