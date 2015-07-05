using prompto.runtime;
using System;

namespace prompto.type
{

    public abstract class NativeType : BaseType
    {

        private static NativeType[] all = null;

        public static NativeType[] getAll()
        {
            if (all == null)
            {
                all = new NativeType[] {
					AnyType.Instance,
					BooleanType.Instance,
					IntegerType.Instance,
					DecimalType.Instance,
					CharacterType.Instance,
					TextType.Instance,
					CodeType.Instance,
					DateType.Instance,
					TimeType.Instance,
					DateTimeType.Instance,
					PeriodType.Instance,
					DocumentType.Instance,
					TupleType.Instance
				};
            }
            return all;
        }
        
        public NativeType(String name)
            : base(name)
        {
        }

        override
        public void checkUnique(Context context)
        {
            // nothing to do
        }

        override
        public void checkExists(Context context)
        {
            // nothing to do
        }

        override
        public bool isMoreSpecificThan(Context context, IType other)
        {
            return false;
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return other == this;
        }

    }

}