
using prompto.parser;
using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.expression;
using prompto.grammar;
using Newtonsoft.Json;
using System.Collections.Generic;
using prompto.store;

namespace prompto.expression
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

		public String GetName()
        {
            return symbol;
        }

		public override string ToString()
		{
			return GetName();
		}

		public abstract void ToDialect (CodeWriter writer);

        public virtual void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }

        public void register(Context context)
        {
            context.registerValue(this);
        }

		public abstract void SetIType(IType type);

		public abstract IType GetIType();

		public abstract IType GetIType(Context context);

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

		
		public virtual IValue GetMember(Context context, String name, bool autoCreate) {
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public void SetMember(Context context, String name, IValue value) {
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public IValue GetItem(Context context, IValue item) {
			throw new NotSupportedException("No item support for " + this.GetType().Name);
		}

		public void SetItem(Context context, IValue item, IValue value) {
			throw new NotSupportedException("No item support for " + this.GetType().Name);
		}

		public virtual void ToJson (Context context, JsonWriter generator, Object instanceId, String fieldName, bool withType, Dictionary<String, byte[]> binaries)
		{
			throw new NotSupportedException("No ToJson support for " + this.GetType().Name);
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


		public bool Contains(Context context, IValue rval)
		{
			return false;
		}


		public ISliceable asSliceable(Context context) {
			return null;
		}

		public object GetStorableData()
		{
			return symbol;
		}

		public void CollectStorables(List<IStorable> storables)
		{
			// nothing to do;
		}

        
    }

}
