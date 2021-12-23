

using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using System.Collections;
using prompto.store;
using prompto.declaration;
using prompto.param;
using prompto.error;

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


		public override Type ToCSharpType(Context context)
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
			ISet<IMethodDeclaration> methods = new HashSet<IMethodDeclaration>();
			switch (name)
			{
				case "toSet":
					methods.Add(new ListToSetMethodDeclaration(GetItemType()));
					return methods;
				case "join":
					methods.Add(JOIN_METHOD);
					return methods;
				case "indexOf":
					methods.Add(INDEX_OF_METHOD);
					return methods;
				case "removeItem":
					methods.Add(REMOVE_ITEM_METHOD);
					return methods;
				case "removeValue":
					methods.Add(REMOVE_VALUE_METHOD);
					return methods;
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

		static IMethodDeclaration JOIN_METHOD = new JoinListMethodDeclaration();

		internal static IParameter ITEM_ARGUMENT = new CategoryParameter(IntegerType.Instance, "item");
		internal static IParameter VALUE_ARGUMENT = new CategoryParameter(AnyType.Instance, "value");

		internal static IMethodDeclaration INDEX_OF_METHOD = new ListIndexOfMethodDeclaration();
		internal static IMethodDeclaration REMOVE_ITEM_METHOD = new RemoveListItemMethodDeclaration();
		internal static IMethodDeclaration REMOVE_VALUE_METHOD = new RemoveListValueMethodDeclaration();

	}

	class JoinListMethodDeclaration : BaseJoinMethodDeclaration {

		protected override IEnumerable<IValue> getItems(Context context)
		{
			ListValue list = (ListValue)getValue(context);
			return list.GetEnumerable(context);
		}
	}

	internal class ListIndexOfMethodDeclaration : BuiltInMethodDeclaration
	{

		public ListIndexOfMethodDeclaration()
		: base("indexOf", ListType.VALUE_ARGUMENT)
		{ }

		public override IValue interpret(Context context)
		{
			ListValue list = (ListValue)getValue(context);
			IValue value = context.getValue("value");
			int idx = list.IndexOf(value);
			if (idx < 0)
				return NullValue.Instance;
			else
				return new IntegerValue(idx + 1);
		}

		public override IType check(Context context)
		{
			return IntegerType.Instance;
		}

	};

	internal class RemoveListItemMethodDeclaration : BuiltInMethodDeclaration
	{

		public RemoveListItemMethodDeclaration()
		: base("removeItem", ListType.ITEM_ARGUMENT)
		{ }

		public override IValue interpret(Context context)
		{
			ListValue list = (ListValue)getValue(context);
			if (!list.IsMutable())
				throw new NotMutableError();
			IntegerValue item = (IntegerValue)context.getValue("item");
			list.RemoveAt((int)item.LongValue - 1);
			return null;
		}

		public override IType check(Context context)
		{
			return VoidType.Instance;
		}

	};

	internal class RemoveListValueMethodDeclaration : BuiltInMethodDeclaration
	{

		public RemoveListValueMethodDeclaration()
		: base("removeValue", ListType.VALUE_ARGUMENT)
		{ }

		public override IValue interpret(Context context)
		{
			ListValue list = (ListValue)getValue(context);
			if (!list.IsMutable())
				throw new NotMutableError();
			IValue value = context.getValue("value");
			list.Remove(value);
			return null;
		}



		public override IType check(Context context)
		{
			return VoidType.Instance;
		}

	};

	internal class ListToSetMethodDeclaration : BuiltInMethodDeclaration
	{

		IType itemType;

		public ListToSetMethodDeclaration(IType itemType)
		: base("toSet")
		{
			this.itemType = itemType;
		}

		public override IValue interpret(Context context)
		{
			ListValue value = (ListValue)getValue(context);
			return value.ToSetValue();
		}

		public override IType check(Context context)
		{
			return new SetType(itemType);
		}

	};

}
