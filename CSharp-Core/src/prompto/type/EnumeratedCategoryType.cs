using System;
using System.Collections.Generic;
using prompto.param;
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

		public override IType AsMutable(bool mutable)
		{
			if (mutable)
            {
				// TODO throw ?
			}
			return this;
		}

		public override IType checkMember(Context context, String name)
		{
			if ("name" == name)
				return TextType.Instance;
			else
				return base.checkMember(context, name);
		}

        public override IType checkStaticMember(Context context, String name)
        {
            if ("symbols" == name)
                return new ListType(this);
            else
                return base.checkStaticMember(context, name);
        }

        public override IValue getStaticMemberValue(Context context, String name)
		{
			IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(GetTypeName());
			if (!(decl is IEnumeratedDeclaration<CategorySymbol>))
				throw new SyntaxError(name + " is not an enumerated type!");
			if ("symbols" == name)
				return ((IEnumeratedDeclaration<CategorySymbol>)decl).getSymbols();
			else
				return base.getStaticMemberValue(context, name);
		}

		public override ISet<IMethodDeclaration> getStaticMemberMethods(Context context, string name)
		{
			if ("symbolOf" == name)
			{
				ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
				list.Add(new CategorySymbolOfMethodDeclaration(this));
				return list;
			}
			else
				return new HashSet<IMethodDeclaration>();
		}
	}

	class CategorySymbolOfMethodDeclaration : BuiltInMethodDeclaration
	{

		internal static IParameter NAME_ARGUMENT = new CategoryParameter(TextType.Instance, "name");

		EnumeratedCategoryType type;

		public CategorySymbolOfMethodDeclaration(EnumeratedCategoryType type)
						: base("symbolOf", NAME_ARGUMENT)
		{
			this.type = type;
		}

		public override IValue interpret(Context context)
		{
			IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(type.GetTypeName());
			if (!(decl is EnumeratedCategoryDeclaration))
				throw new SyntaxError(this.name + " is not an enumerated type!");
			string symbolName = (String)context.getValue("name").GetStorableData();
			return ((EnumeratedCategoryDeclaration)decl).getSymbol(symbolName);
		}



		public override IType check(Context context)
		{
			return type;
		}

	}

}
