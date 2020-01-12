using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.error;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public class TypeSetValidator : BasePropertyValidator
    {
        HashSet<IType> types;

        public TypeSetValidator(HashSet<IType> types)
        {
            this.types = types;
        }


        public override IType GetIType(Context context)
        {
            return AnyType.Instance;
        }


        public override void Validate(Context context, JsxProperty property)
        {
            IType actual = property.check(context);
            foreach (IType type in types)
            {
                if (type.isAssignableFrom(context, actual))
                    return;
            }
            throw new SyntaxError("Illegal assignment " + actual.ToString());
        }

        public override ISet<IMethodDeclaration> GetMethodDeclarations(Context context)
        {
            HashSet<IMethodDeclaration> result = new HashSet<IMethodDeclaration>();
            foreach(IType type in types)
            {
                if (type is MethodType)
                {
                    MethodDeclarationMap decls = context.getRegisteredDeclaration<MethodDeclarationMap>(type.GetTypeName());
                    if (decls != null)
                    {
                        foreach (IMethodDeclaration decl in decls.Values)
                            result.Add(decl);
                    }
                }
            }
            if (result.Count > 0)
                return result;
            else
                return base.GetMethodDeclarations(context);

        }
    }

}
