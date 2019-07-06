using System;
using System.Collections.Generic;
using prompto.argument;
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
			if (!(decl is IEnumeratedDeclaration<CategorySymbol>))
				throw new SyntaxError(name + " is not an enumerated type!");
			if ("symbols" == name)
				return ((IEnumeratedDeclaration<CategorySymbol>)decl).getSymbols();
			else
				throw new SyntaxError("No such member:" + name);
		}

		public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
		{
			if ("symbolOf" == name)
			{
				ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
				list.Add(new CategorySymbolOfMethodDeclaration(this));
				return list;
			}
			else
				return base.getMemberMethods(context, name);
		}
	}

	class CategorySymbolOfMethodDeclaration : BuiltInMethodDeclaration
	{

		internal static IArgument NAME_ARGUMENT = new CategoryArgument(TextType.Instance, "name");

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
