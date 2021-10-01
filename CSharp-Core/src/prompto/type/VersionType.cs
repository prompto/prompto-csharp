using prompto.value;
using prompto.runtime;
using prompto.store;
using System.Collections.Generic;
using System;

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

        
        public override Type ToCSharpType(Context context)
        {
            return typeof(value.VersionValue);
        }


        public override IType checkMember(Context context, String name)
        {
            if ("major" == name)
                return IntegerType.Instance;
            else if ("minor" == name)
                return IntegerType.Instance;
            else if ("fix" == name)
                return IntegerType.Instance;
            else if ("qualifier" == name)
                return TextType.Instance;
            else
                return base.checkMember(context, name);
        }


        public override void checkCompare(Context context, IType other)
        {
            if (other is VersionType)
                return;
            else
                base.checkCompare(context, other);
        }

        
        public override String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

		public override Comparer<IValue> getNativeComparer(bool descending)
		{
			return new VersionComparer(descending);
		}

    }

	class VersionComparer : NativeComparer<value.VersionValue>
    {
        public VersionComparer(bool descending)
            : base(descending)
        {
        }
        
        protected override int DoCompare(value.VersionValue o1, value.VersionValue o2)
        {
            return o1.AsInt().CompareTo(o2.AsInt());
        }
    }

}
