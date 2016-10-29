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

		CategoryType type;
		IExpression predicate;
		IExpression start;
		IExpression end;
		OrderByClauseList orderBy;

		public FetchManyExpression (CategoryType type, IExpression filter, IExpression start, IExpression end, OrderByClauseList orderBy)
		{
			this.type = type;
			this.predicate = filter;
			this.start = start;
			this.end = end;
			this.orderBy = orderBy;
		}


		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				ToEDialect(writer);
				break;
			case Dialect.O:
				ToODialect(writer);
				break;
			case Dialect.S:
				ToSDialect(writer);
				break;
			}
		}

		private void ToSDialect(CodeWriter writer) {
			writer.append("fetch ");
			if(start!=null) {
				writer.append("rows ");
				start.ToDialect(writer);
				writer.append(" to ");
				end.ToDialect(writer);
			} else
				writer.append("all ");
			writer.append(" ( ");
			writer.append(type.GetTypeName().ToString());
			writer.append(" ) ");
			if(predicate!=null) {
				writer.append(" where ");
				predicate.ToDialect(writer);
			}
			if(orderBy!=null)
				orderBy.ToDialect(writer);
		}


		private void ToODialect(CodeWriter writer) {
			writer.append("fetch ");
			if(start==null)
				writer.append("all ");
			writer.append("( ");
			writer.append(type.GetTypeName().ToString());
			writer.append(" ) ");
			if(start!=null) {
				writer.append("rows ( ");
				start.ToDialect(writer);
				writer.append(" to ");
				end.ToDialect(writer);
				writer.append(") ");
			}
			if(predicate!=null) {
				writer.append(" where ( ");
				predicate.ToDialect(writer);
				writer.append(") ");
			}
			if(orderBy!=null)
				orderBy.ToDialect(writer);
		}


		private void ToEDialect(CodeWriter writer) {
			writer.append("fetch ");
			if(start==null)
				writer.append("all ");
			writer.append(type.GetTypeName().ToString());
			if(start!=null) {
				start.ToDialect(writer);
				writer.append(" to ");
				end.ToDialect(writer);
			} 
			if(predicate!=null) {
				writer.append(" where ");
				predicate.ToDialect(writer);
			}
			if(orderBy!=null)
				orderBy.ToDialect(writer);
		}

		public IType check (Context context)
		{
			CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration> (type.GetTypeName ());
			if (decl == null)
				throw new SyntaxError ("Unknown category: " + type.GetTypeName ().ToString ());
			checkFilter (context);
			checkOrderBy (context);
			checkSlice (context);
			return new CursorType (type);
		}

		public void checkOrderBy (Context context)
		{
		}


		public void checkSlice (Context context)
		{
		}

		public void checkFilter (Context context)
		{
			if (predicate == null)
				return;
			if (!(predicate is IPredicateExpression))
				throw new SyntaxError("Filtering expression must be a predicate !");
			IType filterType = predicate.check (context);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError ("Filtering expresion must return a boolean !");
		}

		public IValue interpret (Context context)
		{
			if (predicate!=null && !(predicate is IPredicateExpression))
				throw new SyntaxError("Filtering expression must be a predicate !");
			IStoredEnumerable docs = DataStore.Instance.interpretFetchMany(context, type, start, end, (IPredicateExpression)predicate, orderBy);
			return new Cursor(context, type, docs);
		}

	}
}
