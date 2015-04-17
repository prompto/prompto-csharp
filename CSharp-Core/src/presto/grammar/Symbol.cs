
using presto.parser;
using System;
using presto.runtime;
using presto.type;
using presto.utils;
using presto.value;
using presto.expression;


namespace presto.grammar
{

    public abstract class Symbol : Section, INamed, IValue, IExpression, ISection
    {

        protected String symbol;

        protected Symbol(String symbol)
        {
            this.symbol = symbol;
        }

		public bool IsMutable()
		{
			return false;
		}

        public String getSymbol()
        {
            return symbol;
        }

        public String getName()
        {
            return symbol;
        }

		public abstract void ToDialect (CodeWriter writer);
  
        public void register(Context context)
        {
            context.registerValue(this);
        }

        public abstract IType GetType(Context context);

        public abstract IType check(Context context);

		public abstract IValue interpret(Context context);

		
		public IValue Add(Context context, IValue value) {
			throw new NotSupportedException("Add not supported by " + this.GetType().Name);
		}

		
		public IValue Subtract(Context context, IValue value) {
			throw new NotSupportedException("Subtract not supported by " + this.GetType().Name);
		}

		
		public IValue Multiply(Context context, IValue value) {
			throw new NotSupportedException("Multiply not supported by " + this.GetType().Name);
		}

		
		public IValue Divide(Context context, IValue value) {
			throw new NotSupportedException("Divide not supported by " + this.GetType().Name);
		}

		
		public IValue IntDivide(Context context, IValue value) {
			throw new NotSupportedException("Integer divide not supported by " + this.GetType().Name);
		}

		
		public IValue Modulo(Context context, IValue value) {
			throw new NotSupportedException("Integer divide not supported by " + this.GetType().Name);
		}

		
		public int CompareTo(Context context, IValue value) {
			throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
		}

		
		public IValue GetMember(Context context, String name) {
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public void SetMember(Context context, String name, IValue value) {
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public virtual Object ConvertTo(Type type)
		{
			return this;
		}

		
		public bool Equals(Context context, IValue value) {
			return this.Equals(value);
		}

		public bool Roughly(Context context, IValue value) {
			return this.Equals(value);
		}

		
		public ISliceable asSliceable(Context context) {
			return null;
		}

    }

}
