using System;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public class RequiredForAccessibilityValidator : BasePropertyValidator
	{

		IPropertyValidator validator;

		public RequiredForAccessibilityValidator(IPropertyValidator validator)
		{
			this.validator = validator;
		}

		public override IType GetIType(Context context)
		{
			return validator.GetIType(context);
		}

		public override bool IsRequiredForAccessibility()
		{
			return true;
		}

	
		public override IPropertyValidator OptionalForAccessibility()
		{
			return validator;
		}

    	public override IPropertyValidator RequiredForAccessibility()
		{
			return this;
		}

    	public override void Validate(Context context, JsxProperty property)
		{
			validator.Validate(context, property);
		}

     }
}
