using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

	public class IteratorType : IterableType
	{

		public IteratorType(IType itemType)
			: base(TypeFamily.ITERATOR, itemType, "Iterator<" + itemType.GetTypeName() + ">")
		{
		}

		public override IterableType WithItemType(IType itemType)
		{
			return new IteratorType(itemType);
		}

		public override Type ToCSharpType()
		{
			return typeof(IterableValue);
		}

		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || (other is IteratorType && itemType.isAssignableFrom(context,
									((IteratorType)other).GetItemType()));
		}

		public override bool Equals(Object obj)
		{
			if (obj == this)
				return true;
			if (!(obj is IteratorType))
				return false;
			IteratorType other = (IteratorType)obj;
			return this.GetItemType().Equals(other.GetItemType());
		}

		public override IType checkIterator(Context context)
		{
			return itemType;
		}

		public override IType checkMember(Context context, String name)
		{
			if ("count" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}

        public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
        {
            ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
            switch (name)
            {
                case "toList":
                    list.Add(new IteratorToListMethodDeclaration(GetItemType()));
                    return list;
                default:
                    return base.getMemberMethods(context, name);
            }
        }

    }

    class IteratorToListMethodDeclaration : BuiltInMethodDeclaration
    {

        IType itemType;

        public IteratorToListMethodDeclaration(IType itemType)
        : base("toList")
        {
            this.itemType = itemType;
        }

        public override IValue interpret(Context context)
        {
            IteratorValue value = (IteratorValue)getValue(context);
            return value.ToListValue();
        }



        public override IType check(Context context)
        {
            return new ListType(itemType);
        }

    };
}
