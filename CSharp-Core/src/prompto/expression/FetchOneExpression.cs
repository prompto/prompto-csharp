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

	public class FetchOneExpression : BaseExpression, IExpression
	{

		protected CategoryType type;
		protected IExpression predicate;

		public FetchOneExpression(CategoryType type, IExpression predicate)
		{
			this.type = type;
			this.predicate = predicate;
		}


		public override void ToDialect(CodeWriter writer)
		{
			switch (writer.getDialect())
			{
				case Dialect.E:
				case Dialect.M:
					writer.append("fetch one ");
					if (type != null)
					{
						if (type.Mutable)
							writer.append("mutable ");
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
						if (type.Mutable)
							writer.append("mutable ");
						writer.append(type.GetTypeName().ToString());
						writer.append(") ");
					}
					writer.append("where (");
					predicate.ToDialect(writer);
					writer.append(")");
					break;
			}
		}

		public override IType check(Context context)
		{
			if (!(predicate is IPredicateExpression))
				throw new SyntaxError("Filtering expression must be a predicate !");
			if (type != null)
			{
				CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetTypeName());
				if (decl == null)
					throw new SyntaxError("Unknown category: " + type.GetTypeName().ToString());
				context = context.newInstanceContext((CategoryType)decl.GetIType(context), true);
			}
			IType filterType = predicate.check(context);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError("Filtering expression must return a boolean !");
			if (type != null)
				return type;
			else
				return AnyType.Instance;
		}

		public override IValue interpret(Context context)
		{
			IStore store = DataStore.Instance;
			IQuery query = buildFetchOneQuery(context, store);
			IStored stored = store.FetchOne(query);
			if (stored == null)
				return NullValue.Instance;
			else {
				List<String> categories = (List<String>)stored.GetData("category");
				String actualTypeName = categories.FindLast((v) => true);
				CategoryType type = new CategoryType(actualTypeName);
				return type.newInstance(context, stored);
			}
		}

		public IQuery buildFetchOneQuery(Context context, IStore store)
		{
			IQueryBuilder builder = store.NewQueryBuilder();
			if (type != null)
			{
				AttributeInfo info = new AttributeInfo("category", TypeFamily.TEXT, true, null);
				builder.Verify(info, MatchOp.CONTAINS, type.GetTypeName());
			}
			if (predicate != null)
			{
				if (!(predicate is IPredicateExpression))
					throw new SyntaxError("Filtering expression must be a predicate !");
				((IPredicateExpression)predicate).interpretQuery(context, builder);
			}
			if (type != null && predicate != null)
				builder.And();
			return builder.Build();
		}


	}
}
