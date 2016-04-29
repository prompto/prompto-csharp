using System;
using prompto.runtime;
using prompto.parser;
using prompto.expression;
using prompto.utils;
using prompto.value;
using prompto.type;


namespace prompto.instance
{

    public interface IAssignableInstance
    {

        IType checkAssignValue(Context context, IExpression expression);
		IType checkAssignMember(Context context, String name);
		IType checkAssignItem(Context context, IType itemType);
        void assign(Context context, IExpression expression);
        IValue interpret(Context context);
		void ToDialect(CodeWriter writer, IExpression expression);

    }

}
