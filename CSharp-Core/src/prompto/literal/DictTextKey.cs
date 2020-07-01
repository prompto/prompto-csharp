using System;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
	public class DictTextKey : DictKey
	{
		String text;

		public DictTextKey(String text)
		{
			this.text = text;
		}

        internal override string interpret(Context context)
        {
   			return StringUtils.Unescape(this.text);
		}

        internal override void ToDialect(CodeWriter writer)
        {
			writer.append(this.text);
        }

        public override string ToString()
		{
			return this.text;
		}
	}
}
