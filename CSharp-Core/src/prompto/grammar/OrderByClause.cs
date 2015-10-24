using prompto.parser;
using prompto.grammar;
using prompto.utils;

namespace prompto.grammar
{

	public class OrderByClause : Section
	{

		IdentifierList names;
		bool descending;

		public OrderByClause (IdentifierList names, bool descending)
		{
			this.names = names;
			this.descending = descending;
		}

		public IdentifierList getNames ()
		{
			return names;
		}

		public bool isDescending ()
		{
			return descending;
		}

		public void ToDialect (CodeWriter writer)
		{
			foreach (string name in names) {
				writer.append (name);
				writer.append (".");
			}
			writer.trimLast (1);
			if (descending)
				writer.append (" descending");
		}
	}
}