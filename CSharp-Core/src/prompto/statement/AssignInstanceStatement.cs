using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.grammar;
using prompto.utils;
using prompto.value;
using prompto.instance;

namespace prompto.statement
{

    public class AssignInstanceStatement : SimpleStatement
    {

        IAssignableInstance instance;
        IExpression expression;

        public AssignInstanceStatement(IAssignableInstance instance, IExpression expression)
        {
            this.instance = instance;
            this.expression = expression;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			instance.ToDialect(writer, expression);
			writer.append(" = ");
			expression.ToDialect(writer);
        }

        public IExpression getExpression()
        {
            return expression;
        }

        override
        public IType check(Context context)
        {
			IType valueType = expression.check (context);
			instance.checkAssignValue(context, valueType);
			// Code expressions need to be interpreted as part of full check
			if (valueType == CodeType.Instance)
				instance.assign(context, expression);
			return VoidType.Instance;
        }

        override
        public IValue interpret(Context context)
        {
            instance.assign(context, expression);
            return null;
        }
    }

}
