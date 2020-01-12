using System;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public class RequiredValidator : BasePropertyValidator
	{
		IPropertyValidator validator;

		public RequiredValidator(IPropertyValidator validator)
		{
			this.validator = validator;
		}

        public override IType GetIType(Context context)
        {
            return validator.GetIType(context);
        }

        public override bool IsRequired()
		{
			return true;
		}

		public override IPropertyValidator Optional()
		{
			return validator;
		}

     
        public override IPropertyValidator Required()
		{
			return this;
		}

    
        public override void Validate(Context context, JsxProperty property)
		{
			validator.Validate(context, property);
		}
	}
}
