using System.Collections.Generic;
using System;
using System.Text;
using prompto.utils;

namespace prompto.literal
{

	public class DocEntryList : List<DictEntry>
	{

		public DocEntryList()
		{
		}

		public DocEntryList(List<DictEntry> entries)
			: base(entries)
		{
		}

		public DocEntryList(DictEntry entry)
		{
			this.Add(entry);
		}

		/* for unified grammar */
		public void add(DictEntry entry)
		{
			this.Add(entry);
		}


		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			foreach (DictEntry entry in this)
			{
				sb.Append(entry.ToString());
				sb.Append(", ");
			}
			if (sb.Length > 2)
				sb.Length = sb.Length - 2;
			sb.Append("}");
			return sb.ToString();
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append('{');
			if (this.Count > 0)
			{
				foreach (DictEntry entry in this)
				{
					entry.ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(2);
			}
			writer.append('}');
		}
	}

}
