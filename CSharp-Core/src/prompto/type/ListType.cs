

using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using System.Collections;
using prompto.store;

namespace prompto.type
{

	public class ListType : ContainerType
	{

		public ListType(IType itemType)
			: base(TypeFamily.LIST, itemType, itemType.GetTypeName() + "[]")
		{
		}

		override
		public System.Type ToCSharpType()
		{
			return typeof(ListValue);
		}


		public override IType checkMember(Context context, String name)
		{
			if ("count" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}

		override
		public bool isAssignableTo(Context context, IType other)
		{
			return (other == AnyType.Instance) ||
				(other is ListType && itemType.isAssignableTo(context, ((ListType)other).GetItemType()));
		}

		override
		public bool Equals(Object obj)
		{
			if (obj == this)
				return true;
			if (obj == null)
				return false;
			if (!(obj is ListType))
				return false;
			ListType other = (ListType)obj;
			return this.GetItemType().Equals(other.GetItemType());
		}


		public override IType checkAdd(Context context, IType other, bool tryReverse)
		{
			if ((other is ListType || other is SetType)
				&& this.GetItemType().Equals(((ContainerType)other).GetItemType()))
				return this;
			else
				return base.checkAdd(context, other, tryReverse);
		}

		override
		public IType checkItem(Context context, IType other)
		{
			if (other == IntegerType.Instance)
				return itemType;
			else
				return base.checkItem(context, other);
		}

		override
		public IType checkSlice(Context context)
		{
			return this;
		}


		public override IType checkMultiply(Context context, IType other, bool tryReverse)
		{
			if (other is IntegerType)
				return this;
			return base.checkMultiply(context, other, tryReverse);
		}

		override
		public IType checkContainsAllOrAny(Context context, IType other)
		{
			return BooleanType.Instance;
		}

		override
		public IType checkIterator(Context context)
		{
			return itemType;
		}

		public override IValue ConvertCSharpValueToIValue(Context context, Object value)
		{
			if (value is ICollection)
			{
				ListValue list = new ListValue(itemType);
				foreach (Object item in (ICollection<Object>)value)
					list.Add(itemType.ConvertCSharpValueToIValue(context, item));
				return list;
			}
			else
				return base.ConvertCSharpValueToIValue(context, value);
		}

	}

}
