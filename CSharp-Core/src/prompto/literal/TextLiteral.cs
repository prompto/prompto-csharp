using prompto.runtime;
using System;
using prompto.utils;
using System.IO;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

    public class TextLiteral : Literal<Text>
    {

        public TextLiteral(String text)
			: base(text, new Text(StringUtils.Unescape(text)))
        {
        }

        override
        public IType check(Context context)
        {
            return TextType.Instance;
        }

    }

}
