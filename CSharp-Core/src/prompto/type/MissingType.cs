using prompto.runtime;
using System;

namespace prompto.type
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
        public System.Type ToCSharpType()
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