using System.Collections.Generic;
using System;
using prompto.grammar;
using prompto.error;
using prompto.runtime;

namespace prompto.type
{

    public class TypeMap : Dictionary<String, IType>
    {

        public IType inferType(Context context)
        {
            if (Count == 0)
                return VoidType.Instance;
            IType type = null;
            // first pass: get less specific type
            foreach (IType t in Values)
            {
                if (type == null)
                    type = t;
                else if (type.isAssignableFrom(context, t))
                    continue;
                else if (t.isAssignableFrom(context, type))
                    type = t;
                else
					throw new SyntaxError("Incompatible types: " + type.GetTypeName() + " and " + t.GetTypeName());
            }
            // second pass: check compatible
            foreach (IType t in Values)
            {
                if (!type.isAssignableFrom(context, t))
					throw new SyntaxError("Incompatible types: " + type.GetTypeName() + " and " + t.GetTypeName());
            }
            return type;
        }

    }

}
