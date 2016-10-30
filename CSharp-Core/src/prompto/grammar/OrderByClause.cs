using prompto.parser;
using prompto.grammar;
using prompto.utils;
using prompto.declaration;
using prompto.runtime;
using System;
using prompto.store;

namespace prompto.grammar
{

	public class OrderByClause : Section
	{

		IdentifierList qualifiedName;
		bool descending;

		public OrderByClause (IdentifierList qualifiedName, bool descending)
		{
			this.qualifiedName = qualifiedName;
			this.descending = descending;
		}

		public IdentifierList getNames ()
		{
			return qualifiedName;
		}

		public bool isDescending ()
		{
			return descending;
		}

		public void ToDialect (CodeWriter writer)
		{
			foreach (String name in qualifiedName) {
				writer.append (name);
				writer.append (".");
			}
			writer.trimLast (1);
			if (descending)
				writer.append (" descending");
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			// TODO members
			String name = qualifiedName[0];
			AttributeInfo info = context.findAttribute(name.ToString()).getAttributeInfo();
			builder.AddOrderByClause(info, isDescending());
		}

	}
}