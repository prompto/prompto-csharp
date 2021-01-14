using System.Collections.Generic;
using System;
using prompto.error;
using prompto.runtime;
using prompto.declaration;

namespace prompto.type
{

    public class TypeMap : Dictionary<String, IType>
    {

        public void add(IType type)
        {
            this[type.GetTypeName()] = type;
        }

        public IType inferType(Context context)
        {
            IType inferred = null;
            // first pass: get less specific type
            foreach (IType current in Values)
            {
                if (inferred == null || inferred == NullType.Instance)
                    inferred = current;
                else if (inferred.isAssignableFrom(context, current))
                    inferred = current == DecimalType.Instance ? current : inferred;
                else if (current.isAssignableFrom(context, inferred))
                    inferred = current;
                else
                {
                    IType common = inferCommonBaseType(context, inferred, current);
                    if (common != null)
                        inferred = common;
                    else
                        throw new SyntaxError("Incompatible types: " + inferred.GetTypeName() + " and " + current.GetTypeName());
                }
            }
            if (inferred == null)
                return VoidType.Instance;
            // second pass: check compatible
            foreach (IType t in Values)
            {
                if (!inferred.isAssignableFrom(context, t))
                    throw new SyntaxError("Incompatible types: " + inferred.GetTypeName() + " and " + t.GetTypeName());
            }
            return inferred;
        }

        public IType inferCommonBaseType(Context context, IType type1, IType type2)
        {
            if (type1 is CategoryType && type2 is CategoryType)
                return inferCommonCategoryType(context, (CategoryType)type1, (CategoryType)type2, true);

            else if (type1 is NativeType || type2 is NativeType)
                return AnyType.Instance;

            else
                return null;
        }


        public IType inferCommonCategoryType(Context context, CategoryType type1, CategoryType type2, bool trySwap)
        {
            CategoryDeclaration decl1 = context.getRegisteredDeclaration<CategoryDeclaration>(type1.GetTypeName());
            if (decl1.getDerivedFrom() != null)
            {
                foreach (String name in decl1.getDerivedFrom())
                {
                    CategoryType parentType = new CategoryType(name);
                    if (parentType.isAssignableFrom(context, type2))
                        return parentType;
                }
                // climb up the tree
                foreach (String name in decl1.getDerivedFrom())
                {
                    CategoryType parentType = new CategoryType(name);
                    IType commonType = inferCommonBaseType(context, parentType, type2);
                    if (commonType != null)
                        return commonType;
                }
            }
            if (trySwap)
                return inferCommonCategoryType(context, type2, type1, false);
            else
                return null;
        }

    }

}
