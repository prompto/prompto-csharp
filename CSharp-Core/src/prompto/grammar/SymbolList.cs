using System.Collections.Generic;
using prompto.value;
using prompto.type;
using prompto.utils;
using prompto.runtime;
using System;
using prompto.expression;
using Newtonsoft.Json;
using prompto.store;
using System.Text;

namespace prompto.grammar
{

	public class SymbolList<T> : ExpressionList, IValue, IContainer where T : Symbol
	{

		IType type = MissingType.Instance;

		public SymbolList ()
		{
		}

		public SymbolList (T symbol)
		{
			this.Add (symbol);
		}


		public bool Empty()
		{
			return Length () == 0;
		}

		public long Length()
		{
			return this.Count;
		}

		public bool IsMutable()
		{
			return false;
		}

		public IType ItemType 
		{
			get { return type; } // TODO ?
		}

		public bool HasItem(Context context, IValue iValue)
		{
			throw new NotImplementedException ();
		}

		public IValue GetItem(Context context, IValue item)
		{
			throw new NotImplementedException ();
		}

		public void SetItem(Context context, IValue item, IValue value)
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<IValue> GetEnumerable(Context context)
		{
			return new IValueEnumerable (context, this);
		}

		public virtual void SetIType(IType type) {
			this.type = type;
		}

		public virtual IType GetIType() {
			return this.type;
		}

		public virtual IType GetIType(Context context) {
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

		public virtual IValue GetMemberValue(Context context, String name, bool autoCreate)
		{
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public virtual void SetMemberValue(Context context, String name, IValue value)
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


		public bool Contains(Context context, IValue rval)
		{
			return false;
		}


		public virtual void ToJson (Context context, JsonWriter generator, Object instanceId, String fieldName, bool withType, Dictionary<String, byte[]> binaries)
		{
			throw new NotSupportedException("No ToJson support for " + this.GetType().Name);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			foreach (Symbol s in this)
			{
				sb.Append(s.GetName());
				sb.Append(", ");
			}
			sb.Length -= ", ".Length;
			sb.Append("]");
			return sb.ToString();	
		}

		public void CollectStorables(List<IStorable> storables)
		{
			// nothing to do
		}


		public object GetStorableData()
		{
			throw new NotSupportedException();
		}
	}

}
