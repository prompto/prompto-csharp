using presto.runtime;
using System;
using presto.utils;
using System.IO;
using presto.value;
using presto.type;

namespace presto.literal
{

    public class TextLiteral : Literal<Text>
    {

        public TextLiteral(String text)
            : base(text, Unescape(text))
        {
        }

        private static Text Unescape(String text)
        {
            StreamTokenizer parser = new StreamTokenizer(new StringReader(text));
            parser.NextToken();
            return new Text(parser.StringValue);
        }
        
        override
        public IType check(Context context)
        {
            return TextType.Instance;
        }

    }

}
