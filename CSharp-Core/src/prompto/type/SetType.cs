﻿using prompto.runtime;
using prompto.value;
using System;
using System.Collections.Generic;

namespace prompto.type
{


	public class SetType : ContainerType
	{

		public SetType (IType itemType)
			: base (itemType.GetName () + "<>", itemType)
		{
		}

		public override IType checkIterator (Context context)
		{
			return itemType;
		}

		public override IType checkAdd (Context context, IType other, bool tryReverse)
		{
			if ((other is ListType || other is SetType)
				&& this.GetItemType().Equals(((ContainerType)other).GetItemType()))
				return this;
			return base.checkAdd (context, other, tryReverse);
		}


		public override IType checkMember(Context context, String name)
		{
			if ("length" == name)
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

		public override IType checkContainsAllOrAny (Context context, IType other)
		{
			return BooleanType.Instance;
		}

		public override System.Type ToCSharpType ()
		{
			return typeof(HashSet<Object>);
		}

	}
}