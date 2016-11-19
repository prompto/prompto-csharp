using System;
using prompto.grammar;
using prompto.utils;

namespace prompto.declaration
{
	public class SingletonCategoryDeclaration : ConcreteCategoryDeclaration
	{

		public SingletonCategoryDeclaration (String name, IdentifierList attributes, MethodDeclarationList methods)
			: base (name, attributes, null, methods)
		{
		}

		protected override void categoryTypeToEDialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}

		protected override void categoryTypeToODialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}

		protected override void categoryTypeToMDialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}
	}
}