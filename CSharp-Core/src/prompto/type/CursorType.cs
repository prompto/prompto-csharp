using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

	public class CursorType : ContainerType
	{

		public CursorType (IType itemType)
			: base (TypeFamily.CURSOR, itemType, itemType.GetTypeName() + "[]")
		{
		}


		public override IterableType WithItemType(IType itemType)
		{
			return new CursorType(itemType);
		}

		public override Type ToCSharpType ()
		{
			return typeof(CursorValue);
		}

		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || (other is CursorType && itemType.isAssignableFrom(context, ((CursorType)other).GetItemType()));
		}

		public override bool Equals (object obj)
		{
			if (obj == this)
				return true; 
			if (!(obj is CursorType))
				return false;
			CursorType other = (CursorType)obj;
			return this.GetItemType ().Equals (other.GetItemType ());
		}

		public override IType checkIterator (Context context)
		{
			return itemType;
		}

		public override IType checkMember (Context context, string name)
		{
			if ("count".Equals (name))
				return IntegerType.Instance;
			else if ("totalCount".Equals(name))
				return IntegerType.Instance;
			else
				return base.checkMember (context, name);
		}


		public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
		{
			ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
			switch (name)
			{
				case "toList":
					list.Add(new CursorToListMethodDeclaration(GetItemType()));
					return list;
				default:
					return base.getMemberMethods(context, name);
			}
		}


	}

	class CursorToListMethodDeclaration : BuiltInMethodDeclaration
	{

		IType itemType;

		public CursorToListMethodDeclaration(IType itemType)
		: base("toList")
		{ 
			this.itemType = itemType;
		}

		public override IValue interpret(Context context)
		{
			CursorValue value = (CursorValue)getValue(context);
			return value.ToListValue();
		}



		public override IType check(Context context)
		{
			return new ListType(itemType);
		}

	};

}