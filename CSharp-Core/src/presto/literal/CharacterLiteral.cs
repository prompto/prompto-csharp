using presto.runtime;
using System;
using presto.utils;
using System.IO;
using presto.value;
using presto.type;

namespace presto.literal
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
