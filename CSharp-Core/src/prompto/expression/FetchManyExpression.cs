using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.declaration;
using prompto.utils;
using prompto.error;
using prompto.value;
using prompto.store;
using prompto.grammar;

namespace prompto.expression
{

	public class FetchManyExpression : Section, IExpression
	{

		protected CategoryType type;
		IExpression predicate;
		IExpression first;
		IExpression last;
		OrderByClauseList orderBy;

		public FetchManyExpression(CategoryType type, IExpression filter, IExpression first, IExpression last, OrderByClauseList orderBy)
		{
			this.type = type;
			this.predicate = filter;
			this.first = first;
			this.last = last;
			this.orderBy = orderBy;
		}


		public virtual void ToDialect(CodeWriter writer)
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

		private void ToMDialect(CodeWriter writer)
		{
			writer.append("fetch ");
			if (first != null)
			{
				writer.append("rows ");
				first.ToDialect(writer);
				writer.append(" to ");
				last.ToDialect(writer);
				writer.append(" ");
			}
			else
				writer.append("all ");
			writer.append("( ");
			if (type != null)
			{
				writer.append(type.GetTypeName().ToString());
				writer.append(" ");
			}
			writer.append(") ");
			if (predicate != null)
			{
				writer.append("where ");
				predicate.ToDialect(writer);
				writer.append(" ");
			}
			if (orderBy != null)
				orderBy.ToDialect(writer);
		}


		private void ToODialect(CodeWriter writer)
		{
			writer.append("fetch ");
			if (first == null)
				writer.append("all ");
			if (type != null)
			{
				writer.append("( ");
				writer.append(type.GetTypeName().ToString());
				writer.append(" ) ");
			}
			if (first != null)
			{
				writer.append("rows ( ");
				first.ToDialect(writer);
				writer.append(" to ");
				last.ToDialect(writer);
				writer.append(") ");
			}
			if (predicate != null)
			{
				writer.append(" where ( ");
				predicate.ToDialect(writer);
				writer.append(") ");
			}
			if (orderBy != null)
				orderBy.ToDialect(writer);
		}


		private void ToEDialect(CodeWriter writer)
		{
			writer.append("fetch");
			if (first == null)
				writer.append(" all");
			if (type != null)
			{
				writer.append(" ");
				writer.append(type.GetTypeName());
			}
			if (first != null)
			{
				writer.append(" ");
				first.ToDialect(writer);
				writer.append(" to ");
				last.ToDialect(writer);
			}
			if (predicate != null)
			{
				writer.append(" where ");
				predicate.ToDialect(writer);
			}
			if (orderBy != null)
			{
				writer.append(" ");
				orderBy.ToDialect(writer);
			}
		}

		public virtual IType check(Context context)
		{
			IType type = this.type;
			if(type==null)
				type = AnyType.Instance;
			else
			{
				CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetTypeName());
				if (decl == null)
					throw new SyntaxError("Unknown category: " + type.GetTypeName().ToString());
			}
			checkFilter(context);
			checkOrderBy(context);
			checkSlice(context);
			return new CursorType(type);
		}

		public void checkOrderBy(Context context)
		{
		}


		public void checkSlice(Context context)
		{
		}

		public void checkFilter(Context context)
		{
			if (predicate == null)
				return;
			if (!(predicate is IPredicateExpression))
				throw new SyntaxError("Filtering expression must be a predicate !");
			IType filterType = predicate.check(context);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError("Filtering expresion must return a boolean !");
		}

		public virtual IValue interpret(Context context)
		{
			IStore store = DataStore.Instance;
			IQuery query = buildFetchManyQuery(context, store);
			IStoredEnumerable docs = store.FetchMany(query);
			IType type = this.type != null ? (IType)this.type : AnyType.Instance;
			return new Cursor(context, type, docs);
		}


		private IQuery buildFetchManyQuery(Context context, IStore store)
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
			builder.SetFirst(InterpretLimit(context, first));
			builder.SetLast(InterpretLimit(context, last));
			if (orderBy != null)
				orderBy.interpretQuery(context, builder);
			return builder.Build();
		}

		private long? InterpretLimit(Context context, IExpression exp)
		{
			if (exp == null)
				return null;
			IValue value = exp.interpret(context);
			if (!(value is prompto.value.Integer))
				throw new InvalidValueError("Expecting an Integer, got:" + value.GetIType().GetTypeName());
			return ((prompto.value.Integer)value).IntegerValue;
		}



	}
}
