using System;


using prompto.runtime;

namespace prompto.type
{

	public abstract class IterableType : NativeType
    {

        protected IType itemType;

		protected IterableType(String name, IType itemType)
            : base(name)
        {
            this.itemType = itemType;
        }

		public IType GetItemType()
        {
            return itemType;
        }

		public void SetItemType(IType itemType)
		{
			this.itemType = itemType;
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