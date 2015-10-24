using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.declaration;
using prompto.utils;
using prompto.error;
using prompto.value;
using prompto.store;

namespace prompto.expression
{

	public class FetchOneExpression : Section, IExpression
	{

		CategoryType type;
		IExpression filter;

		public FetchOneExpression (CategoryType type, IExpression filter)
		{
			this.type = type;
			this.filter = filter;
		}


		public void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				writer.append ("fetch one ");
				writer.append (type.GetName ().ToString ());
				writer.append (" where ");
				filter.ToDialect (writer);
				break;
			case Dialect.O:
				writer.append ("fetch one (");
				writer.append (type.GetName ().ToString ());
				writer.append (") where (");
				filter.ToDialect (writer);
				writer.append (")");
				break;
			case Dialect.S:
				writer.append ("fetch one ");
				writer.append (type.GetName ().ToString ());
				writer.append (" where ");
				filter.ToDialect (writer);
				break;
			}
		}

		public IType check (Context context)
		{
			CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration> (type.GetName ());
			if (decl == null)
				throw new SyntaxError ("Unknown category: " + type.GetName ().ToString ());
			Context local = context.newLocalContext ();
			IType filterType = filter.check (local);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError ("Filtering expresion must return a boolean !");
			return type;
		}

		public IValue interpret (Context context)
		{
			Document doc = Store.Instance.fetchOne (context, filter);
			if (doc == null)
				return NullValue.Instance;
			else
				return type.newInstance (context, doc);
		}

	}
}
