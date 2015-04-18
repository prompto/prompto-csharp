using presto.runtime;
using presto.value;
namespace presto.type
{

    public class PeriodType : NativeType
    {

        static PeriodType instance_ = new PeriodType();

         public static PeriodType Instance
        {
            get
            {
                return instance_;
            }
        }

        private PeriodType()
            : base("Duration")
        {
        }

        override
        public System.Type ToSystemType()
        {
            return typeof(Period);
        }


        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is PeriodType) || (other is AnyType);
        }

        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        override
        public IType checkSubstract(Context context, IType other)
        {
            if (other is PeriodType)
                return this;
            return base.checkSubstract(context, other);
        }

        
		public override IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
			return base.checkMultiply(context, other, tryReverse);
        }

        override
        public IType checkCompare(Context context, IType other)
        {
            if (other is PeriodType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

    }
}