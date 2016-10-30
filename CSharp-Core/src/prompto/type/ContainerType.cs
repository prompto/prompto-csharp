using System;


using prompto.runtime;
using prompto.store;

namespace prompto.type
{

	public abstract class ContainerType : IterableType
    {

		protected ContainerType(TypeFamily family, IType itemType, String typeName)
			: base(family, itemType, typeName)
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