using presto.error;
using presto.grammar;
using System;
using presto.parser;
using presto.type;
using presto.runtime;
using presto.expression;
using presto.utils;

namespace presto.value
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
