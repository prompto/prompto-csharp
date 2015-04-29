using System;
using presto.runtime;
using presto.parser;
using presto.expression;
using presto.utils;
using presto.value;


namespace presto.grammar
{

    public interface IAssignableInstance
    {

        void checkAssignValue(Context context, IExpression expression);
        void checkAssignMember(Context context, String name);
        void checkAssignElement(Context context);
        void assign(Context context, IExpression expression);
        IValue interpret(Context context);
		void ToDialect(CodeWriter writer, IExpression expression);

    }

}
