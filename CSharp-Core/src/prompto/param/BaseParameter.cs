
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.expression;
using prompto.value;

namespace prompto.param
{

    public abstract class BaseParameter : IParameter
    {

        protected String name;
		protected bool mutable;

        protected BaseParameter(String name)
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
			if (value is IntegerValue && GetIType(context) == DecimalType.Instance )
				return new value.DecimalValue(((IntegerValue)value).DoubleValue); 
			else if (value is value.DecimalValue && GetIType(context) == IntegerType.Instance )
				return new IntegerValue(((value.DecimalValue)value).LongValue); 
			else
				return value;
		}

        abstract public void ToDialect(CodeWriter writer);
        abstract public String getSignature(Dialect dialect);
        abstract public String getProto();
        abstract public void register(Context context);
        abstract public void check(Context context);
        abstract public IType GetIType(Context context);
  
    }

}
