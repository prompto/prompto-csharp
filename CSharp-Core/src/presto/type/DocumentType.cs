using presto.runtime;
using System;
using presto.value;

namespace presto.type
{

    public class DocumentType : NativeType
    {

        static DocumentType instance_ = new DocumentType();

        public static DocumentType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DocumentType()
            : base("Document")
        {
        }

        override
        public IType CheckMember(Context context, String name)
        {
            return AnyType.Instance;
        }

        override
        public Type ToCSharpType()
        {
            return typeof(Document);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is DocumentType) || (other is AnyType);
        }

    }

}
