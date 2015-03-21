
using presto.runtime;
using System;
using presto.utils;
using presto.value;

namespace presto.grammar
{

    public interface IAttributeConstraint
    {

		void checkValue(Context context, IValue value);
		void ToDialect(CodeWriter writer);

    }

}