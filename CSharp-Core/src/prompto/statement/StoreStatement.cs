using prompto.error;
using prompto.expression;
using prompto.parser;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.utils;
using prompto.value;
using System;

namespace prompto.statement
{


	public class StoreStatement : SimpleStatement
	{

		ExpressionList expressions;

		public StoreStatement (IExpression expression)
		{
			this.expressions = new ExpressionList (expression);
		}

		public StoreStatement (ExpressionList expressions)
		{
			this.expressions = expressions;
		}

		public override void ToDialect (CodeWriter writer)
		{
			writer.append ("store ");
			if (writer.getDialect () == Dialect.E)
				expressions.toDialect (writer);
			else {
				writer.append ('(');
				expressions.toDialect (writer);
				writer.append (')');
			}
		}

		public override String ToString ()
		{
			return "store " + expressions.ToString ();
		}

		public override bool Equals (Object obj)
		{
			if (obj == this)
				return true;
			if (obj == null)
				return false;
			if (!(obj is StoreStatement))
				return false;
			StoreStatement other = (StoreStatement)obj;
			return this.expressions.Equals (other.expressions);
		}

		public override IType check (Context context)
		{
			// TODO check expression
			return VoidType.Instance;
		}

		public override IValue interpret (Context context)
		{
			IStore store = Store.Instance;
			if (store == null)
				store = MemStore.Instance;
			foreach (IExpression exp in expressions) {
				IValue value = exp.interpret (context);
				IStorable storable = null;
				if (value is IInstance)
					storable = ((IInstance)value).getStorable ();
				if (storable == null)
					throw new NotStorableError ();
				if (!storable.Dirty)
					continue;
				Document document = storable.asDocument ();
				store.store (document);
			}
			return null;
		}

	}
}