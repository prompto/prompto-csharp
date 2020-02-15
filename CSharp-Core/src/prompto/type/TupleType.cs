
using prompto.runtime;
using System;
using prompto.value;
using prompto.store;
using System.Collections.Generic;
using prompto.declaration;

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
			: base(TypeFamily.TUPLE, AnyType.Instance, "Tuple")
        {
        }

		public override IterableType WithItemType(IType itemType)
		{
			throw new NotImplementedException();
		}

        
        public override Type ToCSharpType()
        {
            return typeof(TupleValue); 
        }

        
		public override IType checkMember(Context context, String name)
        {
            if ("count" == name)
                return IntegerType.Instance;
            else
                return base.checkMember(context, name);
        }

        
        public override bool isAssignableFrom(Context context, IType other)
        {
			return base.isAssignableFrom(context, other)
			       || other is ListType || other is SetType;
        }

        
        public override IType checkItem(Context context, IType other)
        {
            if (other == IntegerType.Instance)
                return AnyType.Instance;
            else
                return base.checkItem(context, other);
        }


        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is TupleType || other is ListType || other is SetType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        
        public override IType checkContains(Context context, IType other)
        {
            return BooleanType.Instance;
        }

        
        public override IType checkContainsAllOrAny(Context context, IType other)
        {
            return BooleanType.Instance;
        }

        public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
        {
            ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
            switch (name)
            {
                case "join":
                    list.Add(JOIN_METHOD);
                    return list;
                default:
                    return base.getMemberMethods(context, name);
            }
        }

        static IMethodDeclaration JOIN_METHOD = new JoinTupleMethod();
    }

    class JoinTupleMethod : BaseJoinMethod
    {

        protected override IEnumerable<IValue> getItems(Context context)
        {
            TupleValue tuple = (TupleValue)getValue(context);
            return tuple.GetEnumerable(context);
        }
    }
}