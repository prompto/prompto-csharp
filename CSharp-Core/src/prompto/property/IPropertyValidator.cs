using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public interface IPropertyValidator
    {
        bool IsRequired();
        bool IsRequiredForAccessibility();
        void Validate(Context context, JsxProperty prop);
        IPropertyValidator Required();
        IPropertyValidator Optional();
        IPropertyValidator RequiredForAccessibility();
        IPropertyValidator OptionalForAccessibility();
        IType GetIType(Context context);
        ISet<IMethodDeclaration> GetMethodDeclarations(Context context);
    }
}
