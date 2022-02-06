using System.Collections.Generic;
using System.Linq;
using prompto.declaration;
using prompto.error;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public class TypeValidator : BasePropertyValidator
    {
        private IType type;

        public TypeValidator(IType type)
        {
            this.type = type;
        }

        public override IType GetIType(Context context)
        {
            return type;
        }

        public override void Validate(Context context, JsxProperty property)
        {
            IType actual = type is MethodType ? property.checkProto(context, (MethodType)type) : property.check(context);
            if (!type.isAssignableFrom(context, actual))
                throw new SyntaxError("IllegalAssignment: " + type.ToString() + " " + actual.ToString());
        }

        public override ISet<IMethodDeclaration> GetMethodDeclarations(Context context)
        {
            if (type is MethodType) {
                MethodDeclarationMap decls = context.getRegisteredDeclaration<MethodDeclarationMap>(type.GetTypeName());
			    if(decls!=null)
				    return new HashSet<IMethodDeclaration>(decls.Values.Select(m => m.AsReference()));
		    } 
		    return base.GetMethodDeclarations(context);

        }
    }


}
