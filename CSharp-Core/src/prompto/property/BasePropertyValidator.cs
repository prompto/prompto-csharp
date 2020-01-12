using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public abstract class BasePropertyValidator : IPropertyValidator
    {
        public abstract IType GetIType(Context context);
        public abstract void Validate(Context context, JsxProperty prop);

        public virtual bool IsRequired()
        {
            return false;
        }

        public virtual bool IsRequiredForAccessibility()
        {
            return false;
        }

        public virtual IPropertyValidator Optional()
        {
            return this;
        }

        public virtual IPropertyValidator OptionalForAccessibility()
        {
            return this;
        }

        public virtual IPropertyValidator Required()
        {
            return new RequiredValidator(this);
        }

        public virtual IPropertyValidator RequiredForAccessibility()
        {
            return new RequiredForAccessibilityValidator(this);
        }

        public virtual ISet<IMethodDeclaration> GetMethodDeclarations(Context context)
        {
            return new HashSet<IMethodDeclaration>();
        }
    }
}
