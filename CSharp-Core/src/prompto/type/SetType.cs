﻿using prompto.runtime;
using prompto.value;
using System;
using System.Collections.Generic;
using prompto.store;
using prompto.declaration;

namespace prompto.type
{


	public class SetType : ContainerType
	{

		public SetType (IType itemType)
			: base (TypeFamily.SET, itemType, itemType.GetTypeName() + "<>")
		{
		}

		public override IterableType WithItemType(IType itemType)
		{
			return new SetType(itemType);
		}

		public override IType checkIterator (Context context)
		{
			return itemType;
		}

		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || (other is SetType && itemType.isAssignableFrom(context, ((SetType)other).GetItemType()));
		}

		public override IType checkAdd (Context context, IType other, bool tryReverse)
		{
			if ((other is ListType || other is SetType)
				&& this.GetItemType().isAssignableFrom(context, ((ContainerType)other).GetItemType()))
				return this;
			return base.checkAdd (context, other, tryReverse);
		}

		public override IType checkSubstract(Context context, IType other)
		{
			if ((other is ListType || other is SetType)
				&& this.GetItemType().Equals(((ContainerType)other).GetItemType()))
				return this;
			return base.checkSubstract (context, other);
		}

		public override IType checkMember(Context context, String name)
		{
			if ("count" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}

		public override IType checkItem (Context context, IType other)
		{
			if (other == IntegerType.Instance )
				return itemType;
			else
				return base.checkItem (context, other);
		}

		public override void checkContainsAllOrAny (Context context, IType other)
		{
			// nothing to do
		}

		public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
		{
			ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
			switch (name)
			{
				case "toList":
					list.Add(new SetToListMethodDeclaration(GetItemType()));
					return list;
				case "join":
					list.Add(JOIN_METHOD);
					return list;
				default:
					return base.getMemberMethods(context, name);
			}
		}

		public override System.Type ToCSharpType (Context context)
		{
			return typeof(HashSet<Object>);
		}

		static IMethodDeclaration JOIN_METHOD = new JoinSetMethodDeclaration();
	}

	class JoinSetMethodDeclaration : BaseJoinMethodDeclaration
	{

		protected override IEnumerable<IValue> getItems(Context context)
		{
			SetValue set = (SetValue)getValue(context);
			return set.GetEnumerable(context);
		}
	}

	class SetToListMethodDeclaration : BuiltInMethodDeclaration
	{

		IType itemType;

		public SetToListMethodDeclaration(IType itemType)
		: base("toList")
		{
			this.itemType = itemType;
		}

		public override IValue interpret(Context context)
		{
			SetValue value = (SetValue)getValue(context);
			return value.ToListValue();
		}

		public override IType check(Context context)
		{
			return new ListType(itemType);
		}

	};

}