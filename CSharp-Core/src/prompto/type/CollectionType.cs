using System;


using prompto.runtime;

namespace prompto.type
{

    public abstract class CollectionType : NativeType
    {

        protected IType itemType;

        protected CollectionType(String name, IType itemType)
            : base(name)
        {
            this.itemType = itemType;
        }

		public IType GetItemType()
        {
            return itemType;
        }

        override
        public void checkExists(Context context)
        {
            itemType.checkExists(context);
        }

        override
        public IType checkContains(Context context, IType other)
        {
            if (itemType.isAssignableTo(context, other))
                return BooleanType.Instance;
            else
                return base.checkContains(context, other);
        }

    }

}