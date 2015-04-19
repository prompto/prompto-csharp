using presto.runtime;
using System;

namespace presto.type
{

    public class ResourceType : CategoryType
    {

        public ResourceType(String name)
            : base(name)
        {
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is ResourceType))
                return false;
            ResourceType other = (ResourceType)obj;
			return this.GetName().Equals(other.GetName());
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return this.Equals(other);
        }

    }

}