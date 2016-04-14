
using prompto.runtime;
using System;
using prompto.utils;
using prompto.value;

namespace prompto.constraint
{

    public interface IAttributeConstraint
    {

		void checkValue(Context context, IValue value);
		void ToDialect(CodeWriter writer);

    }

}