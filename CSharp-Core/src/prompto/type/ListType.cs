

using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using System.Collections;
using prompto.store;
using prompto.declaration;

namespace prompto.type
{

	public class ListType : ContainerType
	{

		public ListType(IType itemType)
			: base(TypeFamily.LIST, itemType, itemType.GetTypeName() + "[]")
		{
		}

		public override IterableType WithItemType(IType itemType)
		{
			return new ListType(itemType);
		}


		public override Type ToCSharpType()
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


		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || (other is ListType && itemType.isAssignableFrom(context, ((ListType)other).GetItemType()));
		}


		public override bool Equals(Object obj)
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
				&& this.GetItemType().isAssignableFrom(context, ((ContainerType)other).GetItemType()))
				return this;
			else
				return base.checkAdd(context, other, tryReverse);
		}


		public override IType checkSubstract(Context context, IType other)
		{
			if ((other is ListType || other is SetType)
				&& this.GetItemType().Equals(((ContainerType)other).GetItemType()))
				return this;
			else
				return base.checkSubstract(context, other);
		}
	

		public override IType checkItem(Context context, IType other)
		{
			if (other == IntegerType.Instance)
				return itemType;
			else
				return base.checkItem(context, other);
		}


		public override IType checkSlice(Context context)
		{
			return this;
		}


		public override IType checkMultiply(Context context, IType other, bool tryReverse)
		{
			if (other is IntegerType)
				return this;
			return base.checkMultiply(context, other, tryReverse);
		}


		public override void checkContainsAllOrAny(Context context, IType other)
		{
			// nothing to do
		}


		public override IType checkIterator(Context context)
		{
			return itemType;
		}


		public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
		{
			ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
			switch (name)
			{
				case "join":
					list.Add(JOIN_METHOD);
					return list;
				default:
					return base.getMemberMethods(context, name);
			}
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

		static IMethodDeclaration JOIN_METHOD = new JoinListMethod();

	}

    class JoinListMethod : BaseJoinMethod {

		protected override IEnumerable<IValue> getItems(Context context)
		{
			ListValue list = (ListValue)getValue(context);
			return list.GetEnumerable(context);
		}
	}
}
