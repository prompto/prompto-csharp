using prompto.parser;
using System;
using prompto.runtime;
using prompto.expression;
using prompto.value;
using prompto.grammar;
using prompto.type;

namespace prompto.param
{

    public interface IParameter : INamedInstance, IDialectElement
    {

        String getProto();
        String getSignature(Dialect dialect);
        void register(Context context);
        IType check(Context context);
		IValue checkValue(Context context, IExpression expression);
		IExpression DefaultValue { get; }
		bool setMutable(bool set);
		bool isMutable();
    }
}