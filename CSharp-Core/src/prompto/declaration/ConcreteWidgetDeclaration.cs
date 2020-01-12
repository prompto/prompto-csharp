using System;
using prompto.grammar;
using prompto.property;
using prompto.utils;

namespace prompto.declaration
{
	public class ConcreteWidgetDeclaration : ConcreteCategoryDeclaration, IWidgetDeclaration
	{
		PropertyMap properties;

		public ConcreteWidgetDeclaration(String name, String derivedFrom, MethodDeclarationList methods)
			: base(name, null, derivedFrom == null ? null : new IdentifierList(derivedFrom), methods)
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

		public override bool IsAWidget(runtime.Context context)
		{
			return true;
		}

        public override IWidgetDeclaration AsWidget()
        {
            return this;
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
