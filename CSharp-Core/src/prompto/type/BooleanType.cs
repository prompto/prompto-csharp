using System;
using BooleanValue = prompto.value.BooleanValue;
using prompto.runtime;
using System.Collections.Generic;
using prompto.value;
using prompto.store;

namespace prompto.type
{

    public class BooleanType : NativeType
    {

        static BooleanType instance_ = new BooleanType();

    
        public static BooleanType Instance
        {
            get
            {
                return instance_;
            }
        }

        private BooleanType()
			: base(TypeFamily.BOOLEAN)
        {
        }

        
        public override Type ToCSharpType()
        {
            return typeof(System.Boolean);
        }

        
        public override IValue ConvertCSharpValueToIValue(Context context, Object value)
        {
            if (value is bool)
                return BooleanValue.ValueOf((bool)value);
            else if (value is bool?)
                return BooleanValue.ValueOf(((bool?)value).Value);
            else
                return (IValue)value; // TODO for now
        }

		public override Comparer<IValue> getNativeComparer(bool descending)
		{
			return new BooleanComparer(descending);
		}

    }

    class BooleanComparer : NativeComparer<BooleanValue>
    {
        public BooleanComparer(bool descending)
            : base(descending)
        {
        }

		protected override int DoCompare(BooleanValue o1, BooleanValue o2)
		{
	        return o1.Value.CompareTo(o2.Value);
        }
    }

}
