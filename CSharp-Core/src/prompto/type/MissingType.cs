using prompto.runtime;
using System;
using prompto.store;

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
			: base(TypeFamily.MISSING)
        {
        }

        
        public override System.Type ToCSharpType()
        {
            return typeof(Object);
        }

        
        public override bool isAssignableFrom(Context context, IType other)
        {
            return true;
        }

    }
}
