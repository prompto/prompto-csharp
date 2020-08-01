using System.Collections.Generic;
using System;
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
            IType inferred = null;
            // first pass: get less specific type
            foreach (IType t in Values)
            {
                if (t == NullType.Instance)
                    continue;
                else if (inferred == null)
                    inferred = t;
                else if (inferred.isAssignableFrom(context, t))
                    continue;
                else if (t.isAssignableFrom(context, inferred))
                    inferred = t;
                else
                    throw new SyntaxError("Incompatible types: " + inferred.GetTypeName() + " and " + t.GetTypeName());
            }
            if (inferred == null)
                return NullType.Instance;
            // second pass: check compatible
            foreach (IType t in Values)
            {
                if (!inferred.isAssignableFrom(context, t))
					throw new SyntaxError("Incompatible types: " + inferred.GetTypeName() + " and " + t.GetTypeName());
            }
            return inferred;
        }

    }

}
