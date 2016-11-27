using prompto.runtime;
using prompto.store;

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
        
		public NativeType(TypeFamily family)
            : base(family)
        {
        }


		public override IType checkMember(Context context, string name)
		{
			if("text"==name)
				return TextType.Instance;
			else
				return base.checkMember(context, name);
		}
        
        public override void checkUnique(Context context)
        {
            // nothing to do
        }

        
        public override void checkExists(Context context)
        {
            // nothing to do
        }

        
        public override bool isMoreSpecificThan(Context context, IType other)
        {
            return false;
        }


    }

}