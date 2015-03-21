using System;
using presto.grammar;
using presto.type;

namespace presto.runtime
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
            return getName();
        }

        override
		public IType GetType(Context context)
        {
            return new EnumeratedCategoryType("Error");
        }

    }
}
