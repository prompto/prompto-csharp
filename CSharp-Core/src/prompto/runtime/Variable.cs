using prompto.grammar;
using System;
using prompto.type;

namespace prompto.runtime
{

	public class Variable : INamedInstance
    {

        String name;
        IType type;

		public Variable(String name, IType type)
        {
            this.name = name;
			this.type = type;
        }

        
        public override String ToString()
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