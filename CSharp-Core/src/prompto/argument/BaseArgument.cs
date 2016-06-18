
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.expression;
using prompto.value;

namespace prompto.argument
{

    public abstract class BaseArgument : IArgument
    {

        protected String name;
		protected bool mutable;

        protected BaseArgument(String name)
        {
            this.name = name;
        }

        public String GetName()
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
			IValue value = expression.interpret(context);
			if (value is Integer && GetIType(context) == DecimalType.Instance )
				return new value.Decimal(((Integer)value).DecimalValue); 
			else if (value is value.Decimal && GetIType(context) == IntegerType.Instance )
				return new Integer(((value.Decimal)value).IntegerValue); 
			else
				return value;
		}

        abstract public void ToDialect(CodeWriter writer);
        abstract public String getSignature(Dialect dialect);
        abstract public String getProto(Context context);
        abstract public void register(Context context);
        abstract public void check(Context context);
        abstract public IType GetIType(Context context);
  
    }

}
