using System;
using prompto.grammar;
using prompto.utils;

namespace prompto.declaration
{
	public class NativeWidgetDeclaration : NativeCategoryDeclaration, IWidgetDeclaration
	{


		public NativeWidgetDeclaration(String name, NativeCategoryBindingList categoryBindings, MethodDeclarationList methods)
			: base(name, null, categoryBindings, null, methods)
		{
		}

		protected override void categoryTypeToEDialect(CodeWriter writer)
		{
			writer.append("native widget");
		}

		protected override void categoryTypeToODialect(CodeWriter writer)
		{
			writer.append("native widget");
		}

		protected override void categoryTypeToMDialect(CodeWriter writer)
		{
			writer.append("native widget");
		}


		
}

}
