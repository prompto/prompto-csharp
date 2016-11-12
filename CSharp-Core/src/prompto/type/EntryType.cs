using prompto.runtime;
using System;
using prompto.literal;
using prompto.store;

namespace prompto.type
{

	public class EntryType : BaseType
	{

		IType itemType;
		String typeName;

		public EntryType(IType itemType)
			: base(TypeFamily.MISSING)
		{
			this.itemType = itemType;
			this.typeName = itemType.GetTypeName() + "{}[]";
		}

		public override string GetTypeName()
		{
			return typeName;
		}


		public override IType checkMember(Context context, String name)
		{
			if ("key" == name)
				return TextType.Instance;
			else if ("value" == name)
				return itemType;
			else
				return base.checkMember(context, name);
		}

		public IType getItemType()
		{
			return itemType;
		}



		public override Type ToCSharpType()
		{
			return typeof(DictEntry);
		}


		public override void checkUnique(Context context)
		{
			throw new Exception("Should never get there!");
		}


		public override void checkExists(Context context)
		{
			throw new Exception("Should never get there!");
		}


		public override bool isAssignableFrom(Context context, IType other)
		{
			throw new Exception("Should never get there!");
		}


		public override bool isMoreSpecificThan(Context context, IType other)
		{
			throw new Exception("Should never get there!");
		}

	}


}