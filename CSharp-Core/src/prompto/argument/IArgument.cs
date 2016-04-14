using prompto.parser;
using System;
using prompto.runtime;
using prompto.expression;
using prompto.value;
using prompto.grammar;


namespace prompto.argument
{

    public interface IArgument : INamed, IDialectElement
    {

        String getSignature(Dialect dialect);
        String getProto(Context context);
        void register(Context context);
        void check(Context context);
		IValue checkValue(Context context, IExpression expression);
		IExpression DefaultValue { get; }
		bool setMutable(bool set);
		bool isMutable();
    }
}