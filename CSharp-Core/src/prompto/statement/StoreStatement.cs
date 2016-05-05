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

		ExpressionList deletables;
		ExpressionList storables;

		public StoreStatement (ExpressionList deletables, ExpressionList storables)
		{
			this.deletables = deletables;
			this.storables = storables;
		}

		public override void ToDialect (CodeWriter writer)
		{
			if (deletables != null) {
				writer.append ("delete ");
				if (writer.getDialect () == Dialect.E)
					deletables.toDialect (writer);
				else {
					writer.append ('(');
					deletables.toDialect (writer);
					writer.append (')');
				}
				if (storables != null)
					writer.append (" and ");
			}
			if (storables != null) {
				writer.append ("store ");
				if (writer.getDialect () == Dialect.E)
					storables.toDialect (writer);
				else {
					writer.append ('(');
					storables.toDialect (writer);
					writer.append (')');
				}
			}
		}

		public override String ToString ()
		{
			return "store " + storables.ToString ();
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
			return this.storables.Equals (other.storables);
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
			foreach (IExpression exp in storables) {
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