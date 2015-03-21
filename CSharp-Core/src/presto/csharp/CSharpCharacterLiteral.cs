using System;
using presto.runtime;
using presto.value;
using presto.type;

namespace presto.csharp
{

    public class CSharpCharacterLiteral : CSharpLiteral
    {

        Character value;

        public CSharpCharacterLiteral(String text)
			: base(text)
        {
            value = new Character(text[1]);
        }

		override
        public IType check(Context context)
        {
			return new CSharpClassType(typeof(Character));
        }

		override
        public Object interpret(Context context)
        {
            return value;
        }

    }
}
