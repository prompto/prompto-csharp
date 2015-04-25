using System;
using presto.grammar;
using presto.utils;

namespace presto.declaration
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

		protected override void categoryTypeToSDialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}
	}
}