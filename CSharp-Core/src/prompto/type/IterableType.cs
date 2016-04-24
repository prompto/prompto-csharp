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

        override
        public void checkExists(Context context)
        {
            itemType.checkExists(context);
        }

    }

}