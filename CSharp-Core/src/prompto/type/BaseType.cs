using prompto.error;
using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using prompto.utils;
using Newtonsoft.Json.Linq;

namespace prompto.type
{

	public abstract class BaseType : IType
	{

		protected String name;

		protected BaseType (String name)
		{
			this.name = name;
		}

		public String GetName ()
		{
			return name;
		}

		override
        public bool Equals (Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (!(obj is IType))
				return false;
			IType type = (IType)obj;
			return this.GetName ().Equals (type.GetName ());
		}

		override
        public String ToString ()
		{
			return name;
		}

		public virtual void ToDialect (CodeWriter writer)
		{
			writer.append (name);
		}

		public virtual IType checkAdd (Context context, IType other, bool tryReverse)
		{
			if (tryReverse)
				return other.checkAdd (context, this, false);
			else
				throw new SyntaxError ("Cannot add " + this.GetName () + " to " + other.GetName ());
		}

		public virtual IType checkSubstract (Context context, IType other)
		{
			throw new SyntaxError ("Cannot substract " + this.GetName () + " from " + other.GetName ());
		}

		public virtual IType checkMultiply (Context context, IType other, bool tryReverse)
		{
			if (tryReverse)
				return other.checkMultiply (context, this, false);
			else
				throw new SyntaxError ("Cannot multiply " + this.GetName () + " with " + other.GetName ());
		}

		public virtual IType checkDivide (Context context, IType other)
		{
			throw new SyntaxError ("Cannot divide " + this.GetName () + " with " + other.GetName ());
		}

		public virtual IType checkIntDivide (Context context, IType other)
		{
			throw new SyntaxError ("Cannot int divide " + this.GetName () + " with " + other.GetName ());
		}

		public virtual IType checkModulo (Context context, IType other)
		{
			throw new SyntaxError ("Cannot modulo " + this.GetName () + " with " + other.GetName ());
		}

		public virtual IType checkCompare (Context context, IType other)
		{
			throw new SyntaxError ("Cannot compare " + this.GetName () + " to " + other.GetName ());
		}

		public virtual IType checkContains (Context context, IType other)
		{
			throw new SyntaxError (this.GetName () + " cannot contain " + other.GetName ());
		}

		public virtual IType checkContainsAllOrAny (Context context, IType other)
		{
			throw new SyntaxError (this.GetName () + " cannot contain " + other.GetName ());
		}

		public virtual IType checkItem (Context context, IType itemType)
		{
			throw new SyntaxError ("Cannot read item from " + this.GetName ());
		}

		public virtual IType checkSlice (Context context)
		{
			throw new SyntaxError ("Cannot slice " + this.GetName ());
		}

		public virtual IType checkIterator (Context context)
		{
			throw new SyntaxError ("Cannot iterate over " + this.GetName ());
		}

		public virtual IType checkMember (Context context, String name)
		{
			throw new SyntaxError (this.GetName () + " has no member support for:" + name);
		}

		public abstract void checkUnique (Context context);

		public abstract void checkExists (Context context);

		public abstract bool isAssignableTo (Context context, IType other);

		public abstract bool isMoreSpecificThan (Context context, IType other);

		public void checkAssignableTo (Context context, IType other)
		{
			if (!isAssignableTo (context, other))
				throw new SyntaxError ("IType: " + this.GetName () + " is not compatible with: " + other.GetName ());
		}

		public virtual IType checkRange (Context context, IType other)
		{
			throw new SyntaxError ("Cannot create range of " + this.GetName () + " and " + other.GetName ());
		}

		public virtual IRange newRange (Object left, Object right)
		{
			throw new SyntaxError ("Cannot create range of " + this.GetName ());
		}

		public virtual String ToString (Object value)
		{
			return value.ToString ();
		}

		public virtual ListValue sort (Context context, IContainer list)
		{
			throw new Exception ("Unsupported!");
		}

		protected virtual ListValue doSort<T> (Context context, IContainer list, ExpressionComparer<T> cmp)
		{
			IType itemType = ((IterableType)list.GetIType ()).GetItemType ();
			ListValue result = new ListValue (itemType);
			result.AddRange (list.GetEnumerable (context));
			result.Sort (cmp);
			return result;
		}

		public abstract Type ToCSharpType ();

		public virtual IValue ConvertCSharpValueToPromptoValue (Object value)
		{
			return (IValue)value; // TODO for now
		}

		public virtual IValue getMember (Context context, String name)
		{
			throw new SyntaxError ("Cannot read member from " + this.GetName ());
		}

		public virtual IValue ReadJSONValue (Context context, JToken json, Dictionary<String, byte[]> parts)
		{
			throw new SyntaxError ("Cannot read JSON value from " + this.GetName ());
		}

	}

}