using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.declaration;
using prompto.utils;
using prompto.error;
using prompto.value;
using prompto.store;
using System;
using System.Collections.Generic;

namespace prompto.expression
{

	public class FetchOneExpression : Section, IExpression
	{

		CategoryType type;
		IExpression predicate;

		public FetchOneExpression(CategoryType type, IExpression predicate)
		{
			this.type = type;
			this.predicate = predicate;
		}


		public void ToDialect(CodeWriter writer)
		{
			switch (writer.getDialect())
			{
				case Dialect.E:
				case Dialect.S:
					writer.append("fetch one ");
					if (type != null)
					{
						writer.append(type.GetTypeName().ToString());
						writer.append(" ");
					}
					writer.append("where ");
					predicate.ToDialect(writer);
					break;
				case Dialect.O:
					writer.append("fetch one ");
					if (type != null)
					{
						writer.append("(");
						writer.append(type.GetTypeName().ToString());
						writer.append(") ");
					}
					writer.append("where (");
					predicate.ToDialect(writer);
					writer.append(")");
					break;
			}
		}

		public IType check(Context context)
		{
			if (!(predicate is IPredicateExpression))
				throw new SyntaxError("Filtering expression must be a predicate !");
			if (type != null)
			{
				CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetTypeName());
				if (decl == null)
					throw new SyntaxError("Unknown category: " + type.GetTypeName().ToString());
			}
			IType filterType = predicate.check(context);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError("Filtering expression must return a boolean !");
			if (type != null)
				return type;
			else
				return AnyType.Instance;
		}

		public IValue interpret(Context context)
		{
			if (!(predicate is IPredicateExpression))
				throw new SyntaxError("Filtering expression must be a predicate !");
			IStored stored = DataStore.Instance.interpretFetchOne(context, type, (IPredicateExpression)predicate);
			if (stored == null)
				return NullValue.Instance;
			else
			{
				List<String> categories = (List<String>)stored.GetData("category");
				String actualTypeName = categories.FindLast((v) => true);
				CategoryType type = new CategoryType(actualTypeName);
				if (this.type != null)
					type.Mutable = this.type.Mutable;
				return type.newInstance(context, stored);

			}
		}

	}
}
