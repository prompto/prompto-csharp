using System;
using prompto.runtime;
using prompto.value;
using prompto.type;

namespace prompto.csharp
{

    public class CSharpCharacterLiteral : CSharpLiteral
    {

        CharacterValue value;

        public CSharpCharacterLiteral(String text)
			: base(text)
        {
            value = new CharacterValue(text[1]);
        }

		override
        public IType check(Context context)
        {
			return new CSharpClassType(typeof(char?));
        }

		override
        public Object interpret(Context context)
        {
            return value;
        }

    }
}
