using System;
using presto.runtime;
using presto.type;

namespace presto.csharp
{

    public class CSharpBooleanLiteral : CSharpLiteral
    {

        Boolean value;

        public CSharpBooleanLiteral(String text)
			: base(text)
       {
            value = Boolean.Parse(text);
        }

		override
        public IType check(Context context)
        {
			return new CSharpClassType(typeof(Boolean));
        }

		override
        public Object interpret(Context context)
        {
            return value;
        }

    }
}
