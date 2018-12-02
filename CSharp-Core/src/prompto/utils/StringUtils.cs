using System;
using System.Collections.Generic;
using System.IO;
using prompto.value;

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

		public static Character[] ToCharacterArray(String value)
		{
			char[] chars = value.ToCharArray();
			List<Character> list = new List<Character>(chars.Length);
			for (int i = 0; i < chars.Length; i++)
				list.Add(new Character(chars[i]));
			return list.ToArray();
		}


	}
}
