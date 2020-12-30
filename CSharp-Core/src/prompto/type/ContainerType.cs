using System;
using System.Collections.Generic;
using System.Linq;
using prompto.declaration;
using prompto.literal;
using prompto.param;
using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

    public abstract class ContainerType : IterableType
    {

        protected ContainerType(TypeFamily family, IType itemType, String typeName)
            : base(family, itemType, typeName)
        {
        }


        public override void checkContains(Context context, IType other)
        {
            if (other.isAssignableFrom(context, itemType))
                return;
            else
                base.checkContains(context, other);
        }

    }

    abstract class BaseJoinMethodDeclaration : BuiltInMethodDeclaration
    {


        public BaseJoinMethodDeclaration()
            : base("join", new CategoryParameter(TextType.Instance, "delimiter", new TextLiteral("\",\"")))
        {
        }


        public override IValue interpret(Context context) 
        {
            IEnumerable<IValue> items = getItems(context);
            IEnumerable<string> values = items.Select(v => v.ToString());
            String delimiter = (String)context.getValue("delimiter").GetStorableData();
            String joined = String.Join(delimiter, values);
            return new TextValue(joined);
        }

        protected abstract IEnumerable<IValue> getItems(Context context);

        public override IType check(Context context)
        {
            return TextType.Instance;
        }


    }


}