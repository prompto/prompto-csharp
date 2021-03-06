using System;
using prompto.utils;

namespace prompto.css
{

	public class CssText : ICssValue
	{

		String text;

		public CssText(String text)
		{
			this.text = text;
		}

        public override string ToString()
        {
            return text;
        }

        public void ToDialect(CodeWriter writer)
		{
			writer.append(text);
		}

	}
}
