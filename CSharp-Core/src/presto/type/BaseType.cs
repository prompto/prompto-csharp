using presto.error;
using presto.runtime;
using System;
using System.Collections.Generic;
using presto.value;
using presto.utils;

namespace presto.type
{

    public abstract class BaseType : IType
    {

        protected String name;

        protected BaseType(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        override
        public bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (!(obj is IType))
                return false;
            IType type = (IType)obj;
            return this.getName().Equals(type.getName());
        }

        override
        public String ToString()
        {
            return name;
        }

		public void ToDialect(CodeWriter writer) {
			writer.append(name);
		}

		public virtual IType checkAdd(Context context, IType other, bool tryReverse)
        {
			if (tryReverse)
				return other.checkAdd (context, this, false);
			else
				throw new SyntaxError("Cannot add " + this.getName() + " to " + other.getName());
        }

        public virtual IType checkSubstract(Context context, IType other)
        {
            throw new SyntaxError("Cannot substract " + this.getName() + " from " + other.getName());
        }

		public virtual IType checkMultiply(Context context, IType other, bool tryReverse)
        {
			if (tryReverse)
				return other.checkMultiply (context, this, false);
			else
	            throw new SyntaxError("Cannot multiply " + this.getName() + " with " + other.getName());
        }

        public virtual IType checkDivide(Context context, IType other)
        {
            throw new SyntaxError("Cannot divide " + this.getName() + " with " + other.getName());
        }

        public virtual IType checkIntDivide(Context context, IType other)
        {
            throw new SyntaxError("Cannot int divide " + this.getName() + " with " + other.getName());
        }

        public virtual IType CheckModulo(Context context, IType other)
        {
            throw new SyntaxError("Cannot modulo " + this.getName() + " with " + other.getName());
        }

        public virtual IType checkCompare(Context context, IType other)
        {
            throw new SyntaxError("Cannot compare " + this.getName() + " to " + other.getName());
        }

        public virtual IType checkContains(Context context, IType other)
        {
            throw new SyntaxError(this.getName() + " cannot contain " + other.getName());
        }

        public virtual IType checkContainsAllOrAny(Context context, IType other)
        {
            throw new SyntaxError(this.getName() + " cannot contain " + other.getName());
        }

        public virtual IType checkItem(Context context, IType itemType)
        {
            throw new SyntaxError("Cannot read item from " + this.getName());
        }

        public virtual IType checkSlice(Context context)
        {
            throw new SyntaxError("Cannot slice " + this.getName());
        }

        public virtual IType checkIterator(Context context)
        {
            throw new SyntaxError("Cannot iterate over " + this.getName());
        }

        public virtual IType CheckMember(Context context, String name)
        {
            throw new SyntaxError(this.getName() + " has no member support for:" + name);
        }

        public abstract void checkUnique(Context context);

        public abstract void checkExists(Context context);

        public abstract bool isAssignableTo(Context context, IType other);

        public abstract bool isMoreSpecificThan(Context context, IType other);

        public void checkAssignableTo(Context context, IType other)
        {
            if (!isAssignableTo(context, other))
                throw new SyntaxError("IType: " + this.getName() + " is not compatible with: " + other.getName());
        }

        public virtual IType checkRange(Context context, IType other)
        {
            throw new SyntaxError("Cannot create range of " + this.getName() + " and " + other.getName());
        }

        public virtual IRange newRange(Object left, Object right)
        {
            throw new SyntaxError("Cannot create range of " + this.getName());
        }

        public virtual String ToString(Object value)
        {
            return value.ToString();
        }

		public virtual ListValue sort(Context context, IContainer list)
        {
            throw new Exception("Unsupported!");
        }

		protected virtual ListValue doSort<T>(Context context, IContainer list, ExpressionComparer<T> cmp)
        {
			ListValue result = new ListValue(list.ItemType);
			result.AddRange(list.GetItems(context));
            result.Sort(cmp);
            return result;
        }

        public abstract Type ToSystemType();

        public virtual IValue convertSystemValueToPrestoValue(Object value)
        {
            return (IValue)value; // TODO for now
        }

        public virtual IValue getMember(Context context, String name)
        {
		throw new SyntaxError("Cannot read member from " + this.getName());
	}
    }

}