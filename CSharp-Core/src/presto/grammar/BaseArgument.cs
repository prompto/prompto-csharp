
using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.utils;
using presto.expression;
using presto.value;

namespace presto.grammar
{

    public abstract class BaseArgument : IArgument
    {

        protected String name;
		protected bool mutable;

        protected BaseArgument(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

		public bool setMutable(bool set)
		{
			bool result = this.mutable;
			this.mutable = set;
			return result;
		}

		public bool isMutable()
		{
			return this.mutable;
		}

		public IExpression DefaultValue { get; set; }

		public virtual IValue checkValue(Context context, IExpression expression)
        {
			return expression.interpret(context);
        }

        abstract public void ToDialect(CodeWriter writer);
        abstract public String getSignature(Dialect dialect);
        abstract public String getProto(Context context);
        abstract public void register(Context context);
        abstract public void check(Context context);
        abstract public IType GetType(Context context);
  
    }

}
