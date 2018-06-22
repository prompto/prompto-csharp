using System.Collections.Generic;
using System;
using System.Reflection;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace prompto.parser
{

	public class ParserUtils
	{

		public static Dictionary<int, String> ExtractTokenNames(Type klass)
		{
			Dictionary<int, String> result = new Dictionary<int, String>();
			foreach (FieldInfo f in klass.GetFields())
			{
				if (f.FieldType != typeof(Int32))
					continue;
				if (!f.Name.Equals(f.Name.ToUpper()))
					continue;
				FieldAttributes mask = FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal;
				if ((f.Attributes & mask) != mask)
					continue;
				Int32 value = (Int32)f.GetValue(null);
				result[value] = f.Name;
			}
			return result;
		}

		public static String GetFullText(ParserRuleContext ctx)
		{
			IToken start = ctx.Start;
			IToken stop = ctx.Stop;
			if (start == null || stop == null || start.StartIndex < 0 || stop.StopIndex < 0)
				return ctx.GetText();
			Interval interval = Interval.Of(start.StartIndex, stop.StopIndex);
			return start.InputStream.GetText(interval);
		}
	}
}
