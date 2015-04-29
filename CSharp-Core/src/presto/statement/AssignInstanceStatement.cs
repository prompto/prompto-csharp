using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.expression;
using presto.grammar;
using presto.utils;
using presto.value;

namespace presto.statement
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
            instance.checkAssignValue(context, expression);
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
