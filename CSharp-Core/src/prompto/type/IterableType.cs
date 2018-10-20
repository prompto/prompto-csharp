using System;


using prompto.runtime;
using prompto.store;

namespace prompto.type
{

	public abstract class IterableType : NativeType
    {

        protected IType itemType;
		protected String typeName;

		protected IterableType(TypeFamily family, IType itemType, String typeName)
            : base(family)
        {
            this.itemType = itemType;
			this.typeName = typeName;
        }

		public abstract IterableType WithItemType(IType itemType);

		public IType GetItemType()
        {
            return itemType;
        }

		public void SetItemType(IType itemType)
		{
			this.itemType = itemType;
		}


		public override string GetTypeName()
		{
			return typeName;
		}
        
		public override void checkExists(Context context)
        {
            itemType.checkExists(context);
        }

		public override bool isMoreSpecificThan(Context context, IType other) {
			return other is IterableType
				&& itemType.isMoreSpecificThan(context, ((IterableType)other).itemType);
		}

    }

}