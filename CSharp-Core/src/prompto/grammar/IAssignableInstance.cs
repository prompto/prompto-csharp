using System;
using prompto.runtime;
using prompto.parser;
using prompto.expression;
using prompto.utils;
using prompto.value;


namespace prompto.grammar
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
