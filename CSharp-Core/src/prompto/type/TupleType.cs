
using prompto.runtime;
using System;
using prompto.value;

namespace prompto.type
{

	public class TupleType : ContainerType
    {

        static TupleType instance = new TupleType();

        public static TupleType Instance
        {
            get
            {
                return instance;
            }
        }

        private TupleType()
			: base("Tuple", AnyType.Instance)
        {
        }

        override
        public Type ToCSharpType()
        {
            return typeof(TupleValue); 
        }

        override
        public IType CheckMember(Context context, String name)
        {
            if ("length" == name)
                return IntegerType.Instance;
            else
                return base.CheckMember(context, name);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is TupleType) || (other is AnyType);
        }

        override
        public IType checkItem(Context context, IType other)
        {
            if (other == IntegerType.Instance)
                return AnyType.Instance;
            else
                return base.checkItem(context, other);
        }


        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is TupleType || other is ListType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        override
        public IType checkContains(Context context, IType other)
        {
            return BooleanType.Instance;
        }

        override
        public IType checkContainsAllOrAny(Context context, IType other)
        {
            return BooleanType.Instance;
        }

    }

}