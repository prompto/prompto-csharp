using System;
using prompto.grammar;
using prompto.utils;

namespace prompto.declaration
{
	public class ConcreteWidgetDeclaration : ConcreteCategoryDeclaration
	{
		public ConcreteWidgetDeclaration(String name, String derivedFrom, MethodDeclarationList methods)
			: base(name, null, derivedFrom == null ? null : new IdentifierList(derivedFrom), methods)
		{
		}

		public override bool IsAWidget(runtime.Context context)
		{
			return true;
		}

		protected override void categoryTypeToEDialect(CodeWriter writer)
		{
			if (derivedFrom == null)
				writer.append("widget");
			else
				derivedFrom.ToDialect(writer, true);
		}


		protected override void categoryTypeToODialect(CodeWriter writer)
		{
			writer.append("widget");
		}


		protected override void categoryTypeToMDialect(CodeWriter writer)
		{
			writer.append("widget");
		}

	}
}
