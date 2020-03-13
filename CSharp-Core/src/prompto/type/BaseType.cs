using prompto.error;
using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using prompto.utils;
using Newtonsoft.Json.Linq;
using prompto.store;
using prompto.declaration;
using prompto.expression;

namespace prompto.type
{

	public abstract class BaseType : IType
	{

		TypeFamily family;

		protected BaseType(TypeFamily family)
		{
			this.family = family;
		}

		public TypeFamily GetFamily()
		{
			return family;
		}

		public virtual String GetTypeName()
		{
			return family.ToString()[0] + family.ToString().Substring(1).ToLower();
		}


		public virtual IType Anyfy()
		{
			return this;
		}


		public virtual IType AsMutable(Context context, bool mutable)
		{
			if (mutable)
				throw new SyntaxError("Mutable not supported for " + this.GetTypeName());
			else
				return this;
		}

		public virtual IType Resolve(Context context)
		{
			return this;
		}


		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (!(obj is IType))
				return false;
			IType type = (IType)obj;
			return this.GetTypeName().Equals(type.GetTypeName());
		}


		public override String ToString()
		{
			return GetTypeName();
		}

		public virtual void ToDialect(CodeWriter writer)
		{
			ToDialect(writer, false);

		}

        public virtual void ToDialect (CodeWriter writer, bool skipMutable)
		{
			writer.append (GetTypeName());
		}

		public virtual IType checkAdd (Context context, IType other, bool tryReverse)
		{
			if(other is EnumeratedNativeType)
				return checkAdd(context, ((EnumeratedNativeType)other).getDerivedFrom(), tryReverse);
			else if(tryReverse)
				return other.checkAdd (context, this, false);
			else
				throw new SyntaxError ("Cannot add " + this.GetTypeName () + " to " + other.GetTypeName ());
		}

		public virtual IType checkSubstract (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkSubstract(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else 
				throw new SyntaxError ("Cannot substract " + this.GetTypeName () + " from " + other.GetTypeName ());
		}

		public virtual IType checkMultiply (Context context, IType other, bool tryReverse)
		{
			if (other is EnumeratedNativeType)
				return checkMultiply(context, ((EnumeratedNativeType)other).getDerivedFrom(), tryReverse);
			else if (tryReverse)
				return other.checkMultiply (context, this, false);
			else
				throw new SyntaxError ("Cannot multiply " + this.GetTypeName () + " with " + other.GetTypeName ());
		}

		public virtual IType checkDivide (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkDivide(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else
				throw new SyntaxError ("Cannot divide " + this.GetTypeName () + " with " + other.GetTypeName ());
		}

		public virtual IType checkIntDivide (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkIntDivide(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else
				throw new SyntaxError ("Cannot int divide " + this.GetTypeName () + " with " + other.GetTypeName ());
		}

		public virtual IType checkModulo (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkModulo(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else
				throw new SyntaxError ("Cannot modulo " + this.GetTypeName () + " with " + other.GetTypeName ());
		}

		public virtual IType checkCompare (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkCompare(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else
				throw new SyntaxError ("Cannot compare " + this.GetTypeName () + " to " + other.GetTypeName ());
		}

		public virtual IType checkContains (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkContains(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else
				throw new SyntaxError (this.GetTypeName () + " cannot contain " + other.GetTypeName ());
		}

		public virtual IType checkContainsAllOrAny (Context context, IType other)
		{
			if (other is EnumeratedNativeType)
				return checkContainsAllOrAny(context, ((EnumeratedNativeType)other).getDerivedFrom());
			else
				throw new SyntaxError (this.GetTypeName () + " cannot contain " + other.GetTypeName ());
		}

		public virtual IType checkItem (Context context, IType itemType)
		{
			if (itemType is EnumeratedNativeType)
				return checkItem(context, ((EnumeratedNativeType)itemType).getDerivedFrom());
			else
				throw new SyntaxError ("Cannot read item from " + this.GetTypeName ());
		}

		public virtual IType checkSlice (Context context)
		{
			throw new SyntaxError ("Cannot slice " + this.GetTypeName ());
		}

		public virtual IType checkIterator (Context context)
		{
			throw new SyntaxError ("Cannot iterate over " + this.GetTypeName ());
		}

		public virtual IType checkMember (Context context, String name)
		{
			throw new SyntaxError (this.GetTypeName () + " has no member support for:" + name);
		}

        public virtual IType checkStaticMember(Context context, String name)
        {
            throw new SyntaxError(this.GetTypeName() + " has no static member support for:" + name);
        }

        public abstract void checkUnique (Context context);

		public abstract void checkExists (Context context);

		public virtual bool isAssignableFrom(Context context, IType other)
		{
			return this == other
				|| this.Equals(other)
				|| other.Equals(NullType.Instance);
		}

		public abstract bool isMoreSpecificThan (Context context, IType other);

		public void checkAssignableFrom (Context context, IType other)
		{
			if (!isAssignableFrom (context, other))
				throw new SyntaxError ("IType: " + other.GetTypeName () + " is not compatible with: " + this.GetTypeName ());
		}

		public virtual IType checkRange (Context context, IType other)
		{
			throw new SyntaxError ("Cannot create range of " + this.GetTypeName () + " and " + other.GetTypeName ());
		}

		public virtual IRange newRange (Object left, Object right)
		{
			throw new SyntaxError ("Cannot create range of " + this.GetTypeName ());
		}

		public virtual String ToString (Object value)
		{
			return value.ToString ();
		}

		public virtual Comparer<IValue> getComparer (Context context, IExpression key, bool descending)
		{
			throw new Exception ("Unsupported!");
		}

		public abstract Type ToCSharpType ();

		public virtual IValue ConvertCSharpValueToIValue (Context context, Object value)
		{
			return (IValue)value; // TODO for now
		}

		public virtual IValue getStaticMemberValue (Context context, String name)
		{
			throw new SyntaxError ("Cannot read member from " + this.GetTypeName ());
		}

		public virtual ISet<IMethodDeclaration> getMemberMethods(Context context, String name)
		{
			return new HashSet<IMethodDeclaration>();
		}

        public virtual ISet<IMethodDeclaration> getStaticMemberMethods(Context context, String name)
        {
            return new HashSet<IMethodDeclaration>();
        }

        public virtual IValue ReadJSONValue (Context context, JToken json, Dictionary<String, byte[]> parts)
		{
			throw new SyntaxError ("Cannot read JSON value from " + this.GetTypeName ());
		}

	}

}