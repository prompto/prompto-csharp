using System;
using System.IO;

namespace prompto.utils
{
	public abstract class StringUtils
	{
		public static String Unescape(String text)
		{
			StreamTokenizer parser = new StreamTokenizer(new StringReader(text));
			parser.NextToken();
			return parser.StringValue;

		}
	}
}
