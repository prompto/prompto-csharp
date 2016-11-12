using System;
using Boolean = prompto.value.Boolean;
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

        
		public override ListValue sort(Context context, IContainer list, bool descending)
        {
			return this.doSort(context, list, new BooleanComparer(context, descending));
        }

        
        public override IValue ConvertCSharpValueToIValue(Context context, Object value)
        {
            if (value is bool)
                return Boolean.ValueOf((bool)value);
            else if (value is bool?)
                return Boolean.ValueOf(((bool?)value).Value);
            else
                return (IValue)value; // TODO for now
        }


    }

   class BooleanComparer : ExpressionComparer<Boolean>
    {
        public BooleanComparer(Context context, bool descending)
            : base(context, descending)
        {
        }

        override
        protected int DoCompare(Boolean o1, Boolean o2)
        {
            return o1.Value.CompareTo(o2.Value);
        }
    }

}
