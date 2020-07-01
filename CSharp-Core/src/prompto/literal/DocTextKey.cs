using System;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
	public class DocTextKey : DocKey
	{
		String text;

		public DocTextKey(String text)
		{
			this.text = text;
		}

        internal override void ToDialect(CodeWriter writer)
        {
			writer.append(this.text);
		}

        internal override string interpret(Context context)
        {
            return StringUtils.Unescape( this.text);
		}

		public override string ToString()
		{
			return this.text;
		}
	}
}
