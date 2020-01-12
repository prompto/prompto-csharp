using System;
using prompto.grammar;
using prompto.property;
using prompto.runtime;
using prompto.utils;

namespace prompto.declaration
{
	public class NativeWidgetDeclaration : NativeCategoryDeclaration, IWidgetDeclaration
	{

		PropertyMap properties;

		public NativeWidgetDeclaration(String name, NativeCategoryBindingList categoryBindings, MethodDeclarationList methods)
			: base(name, null, categoryBindings, null, methods)
		{
		}

		public void SetProperties(PropertyMap properties)
		{
			this.properties = properties;
		}

		public PropertyMap GetProperties()
		{
			return properties;
		}

        public override bool IsAWidget(Context context)
        {
            return true;
        }

        public override IWidgetDeclaration AsWidget()
        {
            return this;
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
