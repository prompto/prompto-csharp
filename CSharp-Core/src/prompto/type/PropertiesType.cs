using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.property;
using prompto.runtime;
using prompto.store;

namespace prompto.type
{
    public class PropertiesType : BaseType
    {
        PropertyMap properties;

        public PropertiesType(PropertyMap properties)
            : base(TypeFamily.PROPERTIES)
        {
            this.properties = properties;
        }

        public override bool isAssignableFrom(Context context, IType other)
        {
            if (other is DocumentType)
			    return true;
            else
                return base.isAssignableFrom(context, other);
        }

        public override IType checkMember(Context context, string name)
        {
            Property prop = null;
            if (properties.TryGetValue(name.ToString(), out prop))
                return prop.GetValidator().GetIType(context);
            else
                return VoidType.Instance; ; // TODO report warning
        }

        public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
        {
            Property prop = null;
            if (properties.TryGetValue(name.ToString(), out prop))
                return prop.GetValidator().GetMethodDeclarations(context);
            else
                return base.getMemberMethods(context, name);
        }

        public override void checkExists(Context context)
        {
            throw new NotImplementedException();
        }

        public override void checkUnique(Context context)
        {
            throw new NotImplementedException();
        }

        public override bool isMoreSpecificThan(Context context, IType other)
        {
            throw new NotImplementedException();
        }

        public override Type ToCSharpType(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
