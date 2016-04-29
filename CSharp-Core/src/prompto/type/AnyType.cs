
using prompto.runtime;
using System;

namespace prompto.type
{


    public class AnyType : NativeType
    {

        static AnyType instance_ = new AnyType();

        public static AnyType Instance
        {
            get
            {
                return instance_;
            }
        }

        private AnyType()
            : base("any")
        {
        }

        
		public override IType checkMember(Context context, String name)
        {
            return AnyType.Instance;
        }
        
        override
        public Type ToCSharpType()
        {
            return typeof(Object);
        }

        override
        public IType checkItem(Context context, IType itemType)
        {
            return AnyType.Instance; // needed to support lists in Documents
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is AnyType);
        }

    }

}