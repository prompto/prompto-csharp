using System.Collections.Generic;
using presto.value;
using presto.type;
using presto.utils;
using presto.runtime;
using System;

namespace presto.grammar
{

	public class SymbolList<T> : ExpressionList, IValue, IContainer where T : Symbol
	{

		IType type = MissingType.Instance;

		public SymbolList (T symbol)
		{
			this.Add (symbol);
		}

		public bool IsMutable()
		{
			return false;
		}

		public IType Type {
			get { return type; }
			set { this.type = value; }
		}

		public IType ItemType 
		{
			get { return type; } // TODO ?
		}

		public bool HasItem(Context context, IValue iValue)
		{
			return false; // TODO
		}

		public IValue GetItem(Context context, IValue item)
		{
			return null; // TODO
		}

		public IEnumerable<IValue> GetItems(Context context)
		{
			return new IValueEnumerable (context, this);
		}

		public virtual IType GetType(Context context) {
			return this.type;
		}

		public virtual IValue Add(Context context, IValue value)
		{
			throw new NotSupportedException("Add not supported by " + this.GetType().Name);
		}

		public virtual IValue Subtract(Context context, IValue value)
		{
			throw new NotSupportedException("Subtract not supported by " + this.GetType().Name);
		}

		public virtual IValue Multiply(Context context, IValue value)
		{
			throw new NotSupportedException("Multiply not supported by " + this.GetType().Name);
		}

		public virtual IValue Divide(Context context, IValue value)
		{
			throw new NotSupportedException("Divide not supported by " + this.GetType().Name);
		}

		public virtual IValue IntDivide(Context context, IValue value)
		{
			throw new NotSupportedException("Integer divide not supported by " + this.GetType().Name);
		}

		public virtual IValue Modulo(Context context, IValue value)
		{
			throw new NotSupportedException("Integer divide not supported by " + this.GetType().Name);
		}

		public virtual Int32 CompareTo(Context context, IValue value)
		{
			throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
		}

		public virtual IValue GetMember(Context context, String name)
		{
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public virtual void SetMember(Context context, String name, IValue value)
		{
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public virtual Object ConvertTo(Type type)
		{
			return this;
		}

		public virtual bool Equals(Context context, IValue rval)
		{
			return this.Equals (rval);
		}

		public virtual bool Roughly(Context context, IValue rval)
		{
			return this.Equals (context, rval);
		}

	}

}
