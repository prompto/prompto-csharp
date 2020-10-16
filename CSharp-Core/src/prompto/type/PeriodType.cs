using System;
using prompto.runtime;
using prompto.store;
using prompto.value;
namespace prompto.type
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
			: base(TypeFamily.PERIOD)
        {
        }

        
        public override Type ToCSharpType(Context context)
        {
            return typeof(PeriodValue);
        }


        public override IType checkMember(Context context, String name)
        {
            switch (name)
            {
                case "years":
                case "months":
                case "weeks":
                case "days":
                case "hours":
                case "minutes":
                case "seconds":
                case "milliseconds":
                    return IntegerType.Instance;
                default:
                    return base.checkMember(context, name);
            }
        }


        public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        
        public override IType checkSubstract(Context context, IType other)
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

        
        public override void checkCompare(Context context, IType other)
        {
            if (other is PeriodType)
                return;
            else
                base.checkCompare(context, other);
        }

    }
}
