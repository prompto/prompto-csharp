
using prompto.runtime;
using System;
using prompto.store;

namespace prompto.type
{


    public class DbIdType : NativeType
    {

        static DbIdType instance_ = new DbIdType();

        public static DbIdType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DbIdType()
            : base(TypeFamily.DBID)
        {
        }

        public override string GetTypeName()
        {
            return "DbId";
        }

        public override Type ToCSharpType(Context context)
        {
            return typeof(Object);
        }
      
        public override bool isAssignableFrom(Context context, IType other)
        {
            return base.isAssignableFrom(context, other) || other is NativeType;
        }

        
    }

}