using presto.grammar;
using System;
using presto.type;
using presto.declaration;
namespace presto.runtime
{

	public class Attribute : INamed
    {

        String name;

        public Attribute(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        public IType GetType(Context context)
        {
            AttributeDeclaration declaration = context.getRegisteredDeclaration<AttributeDeclaration>(name);
            return declaration.GetType(context);
        }

    }
}
