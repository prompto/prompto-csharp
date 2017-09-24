using prompto.value;
using prompto.runtime;
using System;
using prompto.store;

namespace prompto.type
{

    public class VersionType : NativeType
    {

        static VersionType instance_ = new VersionType();

        public static VersionType Instance
        {
            get
            {
                return instance_;
            }
        }

        private VersionType()
			: base(TypeFamily.VERSION)
        {
        }

        
        public override Type ToCSharpType()
        {
            return typeof(value.Version);
        }


        override
        public IType checkCompare(Context context, IType other)
        {
            if (other is VersionType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
		public ListValue sort(Context context, IContainer list, bool descending)
        {
			return this.doSort(context, list, new VersionComparer(context, descending));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }
    }

	class VersionComparer : ExpressionComparer<value.Version>
    {
        public VersionComparer(Context context, bool descending)
            : base(context, descending)
        {
        }
        override
        protected int DoCompare(value.Version o1, value.Version o2)
        {
            return o1.AsInt().CompareTo(o2.AsInt());
        }
    }

}
