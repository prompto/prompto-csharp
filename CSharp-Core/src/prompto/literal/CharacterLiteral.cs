using prompto.runtime;
using System;
using prompto.utils;
using System.IO;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

    public class CharacterLiteral : Literal<Character>
    {

        public CharacterLiteral(String text)
            : base(text, new Character(Unescape(text)))
        {
        }

        private static char Unescape(String text)
        {
            StreamTokenizer parser = new StreamTokenizer(new StringReader(text));
            parser.NextToken();
            return parser.StringValue[0];
        }

        override
        public IType check(Context context)
        {
            return CharacterType.Instance;
        }

    }

}
