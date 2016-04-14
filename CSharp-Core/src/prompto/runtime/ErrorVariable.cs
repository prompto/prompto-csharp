using System;
using prompto.grammar;
using prompto.type;

namespace prompto.runtime
{

    public class ErrorVariable : Variable
    {

        public ErrorVariable(String name)
            : base(name, null)
        {
        }

        override
        public String ToString()
        {
			return GetName();
        }

        override
		public IType GetIType(Context context)
        {
            return new EnumeratedCategoryType("Error");
        }

    }
}
