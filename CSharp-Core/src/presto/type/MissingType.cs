using presto.runtime;
using System;

namespace presto.type
{

    public class MissingType : NativeType
    {

        static MissingType instance = new MissingType();

        public static MissingType Instance
        {
            get
            {
                return instance;
            }
        }

        private MissingType()
            : base("*")
        {
        }

        override
        public System.Type ToSystemType()
        {
            return typeof(Object);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return true;
        }

    }
}
