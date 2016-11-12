using prompto.runtime;
using System;
using prompto.store;

namespace prompto.type
{

    public class ResourceType : CategoryType
    {

        public ResourceType(String name)
            : base(TypeFamily.RESOURCE, name)
        {
        }

        
        public override bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is ResourceType))
                return false;
            ResourceType other = (ResourceType)obj;
			return this.GetTypeName().Equals(other.GetTypeName());
        }

        
    }

}