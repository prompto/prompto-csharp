using System;
using presto.runtime;
using presto.parser;
using presto.expression;
using presto.utils;


namespace presto.grammar
{

    public interface IAssignableInstance
    {

        void checkAssignValue(Context context, IExpression expression);
        void checkAssignMember(Context context, String name);
        void checkAssignElement(Context context);
        void assign(Context context, IExpression expression);
        Object interpret(Context context);
        void ToDialect(CodeWriter writer);

    }

}
