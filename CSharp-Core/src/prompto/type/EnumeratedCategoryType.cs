using System;
using prompto.declaration;
using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

    public class EnumeratedCategoryType : CategoryType
    {

        public EnumeratedCategoryType(String name)
            : base(TypeFamily.ENUMERATED, name)
        {
        }

        
		public override IType checkMember(Context context, String name)
        {
			if ("symbols" == name)
				return new ListType(this);
            else if ("name" == name)
                return TextType.Instance;
            else
                return base.checkMember(context, name);
        }

		public override IValue getMemberValue(Context context, String name)
		{
			IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(GetTypeName());
			if(!(decl is IEnumeratedDeclaration<CategorySymbol>))
				throw new SyntaxError(name + " is not an enumerated type!");
			if ("symbols" == name)
				return ((IEnumeratedDeclaration<CategorySymbol>)decl).getSymbols();
			else
				throw new SyntaxError("No such member:" + name);
		}

    }

}
