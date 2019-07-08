using System;
using prompto.expression;
using prompto.literal;
using prompto.utils;

namespace prompto.grammar
{
	public class Annotation
	{
		string name;
		DictEntryList arguments;

		public Annotation(string name, DictEntryList arguments)
		{
			this.name = name;
           	this.arguments = arguments;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(name);
			if (arguments != null && arguments.Count > 0)
			{
				writer.append("(");
				foreach (DictEntry entry in arguments)
				{
					if (entry.getKey() != null)
					{
						entry.getKey().ToDialect(writer);
						writer.append(" = ");
					}
					entry.getValue().ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(", ".Length);
				writer.append(")");
			}
			writer.newLine();
		}

	}
}
