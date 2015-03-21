using presto.parser;
using System;
using presto.runtime;
using presto.expression;
using presto.value;


namespace presto.grammar
{

    public interface IArgument : INamed, IDialectElement
    {

        String getSignature(Dialect dialect);
        String getProto(Context context);
        void register(Context context);
        void check(Context context);
		IValue checkValue(Context context, IExpression expression);
		IExpression DefaultValue { get; }
    }
}