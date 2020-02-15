using prompto.runtime;
using System;
using prompto.utils;
using System.IO;
using prompto.value;
using prompto.type;
using prompto.parser;

namespace prompto.literal
{

    public class TextLiteral : Literal<TextValue>
    {
		public TextLiteral(String text)
			: base(text, new TextValue(StringUtils.Unescape(text)))
        {
        }


        public override IType check(Context context)
        {
            return TextType.Instance;
        }

    }

}
