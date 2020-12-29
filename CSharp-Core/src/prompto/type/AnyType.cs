
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

        public override IType checkItem(Context context, IType itemType)
        {
            return DocumentType.Instance.checkItem(context, itemType); // needed to support lists in Documents
        }


        public override IType checkMember(Context context, String name)
        {
            return DocumentType.Instance.checkMember(context, name); // needed to support members in Documents
        }
        
        
        public override Type ToCSharpType(Context context)
        {
            return typeof(Object);
        }

        
        
        public override bool isAssignableFrom(Context context, IType other)
        {
            return true;
        }

        
    }

}