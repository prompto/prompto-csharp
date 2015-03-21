using presto.grammar;
using System;
using presto.expression;
using presto.type;

namespace presto.runtime
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

        public String getName()
        {
            return name;
        }

		public virtual IType GetType(Context context)
        {
            return type;
        }

    }
}