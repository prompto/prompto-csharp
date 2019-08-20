using prompto.parser;
using System;
using prompto.runtime;
using prompto.expression;
using prompto.value;
using prompto.grammar;


namespace prompto.param
{

    public interface IParameter : INamed, IDialectElement
    {

        String getProto();
        String getSignature(Dialect dialect);
        void register(Context context);
        void check(Context context);
		IValue checkValue(Context context, IExpression expression);
		IExpression DefaultValue { get; }
		bool setMutable(bool set);
		bool isMutable();
    }
}