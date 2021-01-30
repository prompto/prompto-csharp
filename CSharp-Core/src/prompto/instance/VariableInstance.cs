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

        public override string ToString()
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

		public IType checkAssignValue(Context context, IType valueType)
        {
            INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual == null)
            {
				context.registerValue(new Variable(name, valueType));
            }
            else
            {
                // need to check type compatibility
                IType actualType = actual.GetIType(context);
				actualType.checkAssignableFrom(context, valueType);
				valueType = actualType;
            }
			return valueType;
        }

        public IType checkAssignMember(Context context, String memberName, IType valueType)
        {
            INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual == null)
                throw new SyntaxError("Unknown variable:" + this.name);
            IType thisType = actual.GetIType(context);
            if (thisType is DocumentType)
                return valueType;
            else
            {
                if (thisType is CategoryType && !((CategoryType)thisType).Mutable)
                    throw new SyntaxError("Not mutable:" + this.name);
                IType requiredType = thisType.checkMember(context, memberName);
                if (requiredType != null && !requiredType.isAssignableFrom(context, valueType))
                    throw new SyntaxError("Incompatible types:" + requiredType.GetTypeName() + " and " + valueType.GetTypeName());
                return valueType;
            }
        }


		public IType checkAssignItem(Context context, IType itemType, IType valueType)
        {
			INamed actual = context.getRegisteredValue<INamed>(name);
			if(actual==null) 
				throw new SyntaxError("Unknown variable:" + this.name);
			IType parentType = actual.GetIType(context);
			return parentType.checkItem(context, itemType);
        }

        public void assign(Context context, IExpression expression)
        {
            IValue value = expression.interpret(context);
			if (context.getRegisteredValue<INamed>(name) == null)
			{
				IType type = expression.check(context);
				context.registerValue(new Variable(name, type));
			}
            context.setValue(name, value);
        }

        public IValue interpret(Context context)
        {
            return context.getValue(name);
        }

    }

}