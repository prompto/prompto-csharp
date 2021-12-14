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
		protected List<string> include;

		public FetchOneExpression(CategoryType type, IExpression predicate, List<string> include)
		{
			this.type = type;
			this.predicate = predicate;
			this.include = include;
		}


		public override void ToDialect(CodeWriter writer)
		{
			switch (writer.getDialect())
			{
				case Dialect.E:
					ToEDialect(writer);
					break;
				case Dialect.O:
					ToODialect(writer);
					break;
				case Dialect.M:
					ToMDialect(writer);
					break;
			}
		}

		public void ToEDialect(CodeWriter writer)
        {
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
			if (include != null)
			{
				writer.append(" include ");
				if (include.Count == 1)
					writer.append(include[0]);
				else
				{
					for (int i = 0; i < include.Count - 1; i++)
					{
						writer.append(include[i]).append(", ");
					}
					writer.trimLast(", ".Length);
					writer.append(" and ").append(include[include.Count - 1]);
				}
			}
		}

		public void ToMDialect(CodeWriter writer)
		{
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
			if (include != null)
			{
				writer.append(" include ");
				foreach (string name in include)
				{
					writer.append(name).append(", ");
				}
				writer.trimLast(", ".Length);
			}
		}

		public void ToODialect(CodeWriter writer)
		{
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
			if (include != null)
			{
				writer.append(" include (");
				foreach (string name in include)
				{
					writer.append(name).append(", ");
				}
				writer.trimLast(", ".Length);
				writer.append(")");
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
			if(include != null)
            {
				foreach (string name in include)
                {
					if(context.getRegisteredDeclaration<AttributeDeclaration>(name)==null) 
                    {
						throw new SyntaxError("Unknown attribute: " + name);
					}
				}
			}
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
				if(this.type != null)
					type.Mutable = this.type.Mutable;
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
            if (include != null)
            {
				builder.Project(include);
			}
			return builder.Build();
		}


	}
}
