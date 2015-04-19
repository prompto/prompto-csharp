using System.Collections.Generic;
using System;
using presto.grammar;
using presto.error;
using presto.runtime;

namespace presto.type
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
                else if (t.isAssignableTo(context, type))
                    continue;
                else if (type.isAssignableTo(context, t))
                    type = t;
                else
					throw new SyntaxError("Incompatible types: " + type.GetName() + " and " + t.GetName());
            }
            // second pass: check compatible
            foreach (IType t in Values)
            {
                if (!t.isAssignableTo(context, type))
					throw new SyntaxError("Incompatible types: " + type.GetName() + " and " + t.GetName());
            }
            return type;
        }

    }

}
