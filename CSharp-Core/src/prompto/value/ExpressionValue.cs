using prompto.error;
using prompto.grammar;
using System;
using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.expression;
using prompto.utils;

namespace prompto.value
{

	public class ExpressionValue : BaseValue, IExpression
    {

		IValue value;

		public ExpressionValue(IType type, IValue value)
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
