using System;
using prompto.property;

namespace prompto.declaration
{
	public interface IWidgetDeclaration : IDeclaration
	{
		void SetProperties(PropertyMap properties);
		PropertyMap GetProperties();

	}
}
