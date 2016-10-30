using prompto.error;
using prompto.expression;
using prompto.parser;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.utils;
using prompto.value;
using System;
using System.Collections.Generic;

namespace prompto.statement
{


	public class StoreStatement : SimpleStatement
	{

		ExpressionList toDelete;
		ExpressionList toStore;

		public StoreStatement (ExpressionList toDelete, ExpressionList toStore)
		{
			this.toDelete = toDelete;
			this.toStore = toStore;
		}

		public override void ToDialect (CodeWriter writer)
		{
			if (toDelete != null) {
				writer.append ("delete ");
				if (writer.getDialect () == Dialect.E)
					toDelete.toDialect (writer);
				else {
					writer.append ('(');
					toDelete.toDialect (writer);
					writer.append (')');
				}
				if (toStore != null)
					writer.append (" and ");
			}
			if (toStore != null) {
				writer.append ("store ");
				if (writer.getDialect () == Dialect.E)
					toStore.toDialect (writer);
				else {
					writer.append ('(');
					toStore.toDialect (writer);
					writer.append (')');
				}
			}
		}

		public override String ToString ()
		{
			return "store " + toStore.ToString ();
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
			return this.toStore.Equals (other.toStore);
		}

		public override IType check (Context context)
		{
			// TODO check expression
			return VoidType.Instance;
		}

		public override IValue interpret (Context context)
		{
			List<object> deletables = null;
			if (toDelete != null)
			{
				deletables = new List<object>();
				toDelete.ForEach((exp) => CollectDeletables(context, exp, deletables));
			}
			List<IStorable> storables = null;
			if (toStore != null)
			{
				storables = new List<IStorable>();
				toStore.ForEach((exp) => CollectStorables(context, exp, storables));
			}
			if (deletables != null || storables != null)
				DataStore.Instance.store(deletables, storables);
			return null;
		}

		void CollectDeletables(Context context, IExpression exp, List<object> deletables)
		{
			IValue value = exp.interpret(context);
			if (value == NullValue.Instance)
				return;
			else if (value is IInstance)
			{
				IValue dbId = ((IInstance)value).GetMember(context, "dbId", false);
				if (dbId != null && dbId != NullValue.Instance)
					deletables.add(dbId.GetStorableData());
			}
			else if (value is IEnumerable<IValue>)
			{
				IEnumerator<IValue> iter = ((IEnumerable<IValue>)value).GetEnumerator();
				while (iter.MoveNext())
				{
					IValue item = iter.Current;
					if (item is IInstance)
					{
						IValue dbId = ((IInstance)value).GetMember(context, "dbId", false);
						if (dbId != null && dbId!=NullValue.Instance)
							deletables.add(dbId.GetStorableData());
					}
				}
			}
			else
				throw new NotSupportedException("Can't delete " + value.GetType());
		}

		void CollectStorables(Context context, IExpression exp, List<IStorable> storables)
		{
			IValue value = exp.interpret(context);
			value.CollectStorables(storables);
		}
	}
}
