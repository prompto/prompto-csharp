using System;
using Boolean = presto.value.Boolean;
using presto.runtime;
using System.Collections.Generic;
using presto.value;

namespace presto.type
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
            : base("Boolean")
        {
        }

        override
        public Type ToSystemType()
        {
            return typeof(System.Boolean);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is BooleanType) || (other is AnyType);
        }

 
        override
		public ListValue sort(Context context, IContainer list)
        {
			return this.doSort(context, list, new BooleanComparer(context));
        }

        override
        public IValue convertSystemValueToPrestoValue(Object value)
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
        public BooleanComparer(Context context)
            : base(context)
        {
        }

        override
        protected int DoCompare(Boolean o1, Boolean o2)
        {
            return o1.Value.CompareTo(o2.Value);
        }
    }

}
