using prompto.grammar;
using System;
using prompto.expression;
using prompto.type;

namespace prompto.runtime
{

	public class Variable : INamed
    {

        String name;
        IType type;

		public Variable(String name, IType type)
        {
            this.name = name;
			this.type = type;
        }

        override
        public String ToString()
        {
			return name;
        }

		public String GetName()
        {
            return name;
        }

		public virtual IType GetIType(Context context)
        {
            return type;
        }

    }
}