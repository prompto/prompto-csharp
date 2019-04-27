using System;
using prompto.type;
using prompto.runtime;
using prompto.value;
using prompto.utils;

namespace prompto.expression
{

	public class ValueExpression : BaseValue, IExpression
    {

		IValue value;

		public ValueExpression(IType type, IValue value)
			: base(type)
        {
            this.value = value;
        }

        public IType check(Context context)
        {
            return type;
        }

        public IValue interpret(Context context)
        {
            return value;
        }

        override
        public String ToString()
        {
            return type.ToString(value);
        }

        public void ToDialect(CodeWriter writer)
        {
			writer.append(value.ToString()); // value has no dialect
        }


    }

}
