
using prompto.runtime;
using System;
using prompto.store;

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
			: base(TypeFamily.ANY)
        {
        }

		public override string GetTypeName()
		{
			return "any";
		}

		public override IType checkMember(Context context, String name)
        {
            return AnyType.Instance;
        }
        
        
        public override Type ToCSharpType()
        {
            return typeof(Object);
        }

        
        public override IType checkItem(Context context, IType itemType)
        {
            return AnyType.Instance; // needed to support lists in Documents
        }

        
        public override bool isAssignableFrom(Context context, IType other)
        {
            return true;
        }

        
    }

}