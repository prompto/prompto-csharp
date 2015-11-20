using System;


using prompto.runtime;

namespace prompto.type
{

	public abstract class ContainerType : IterableType
    {

        protected ContainerType(String name, IType itemType)
			: base(name, itemType)
        {
        }

        
		public override IType checkContains(Context context, IType other)
        {
            if (itemType.isAssignableTo(context, other))
                return BooleanType.Instance;
            else
                return base.checkContains(context, other);
        }

    }

}