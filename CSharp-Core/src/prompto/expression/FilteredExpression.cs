using prompto.parser;
using System;
using prompto.runtime;
using prompto.error;
using prompto.type;
using prompto.value;
using prompto.utils;

namespace prompto.expression
{

	public class FilteredExpression : Section, IExpression
	{

		String itemName;
		IExpression source;
		IExpression predicate;

		public FilteredExpression(String name, IExpression source, IExpression predicate)
		{
			this.itemName = name;
			this.source = source;
			this.predicate = predicate;
		}


		public IExpression Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}


		public void ToDialect(CodeWriter writer)
		{
			switch (writer.getDialect())
			{
				case Dialect.E:
				case Dialect.S:
					source.ToDialect(writer);
					writer.append(" filtered with ");
					writer.append(itemName);
					writer.append(" where ");
					predicate.ToDialect(writer);
					break;
				case Dialect.O:
					writer.append("filtered (");
					source.ToDialect(writer);
					writer.append(") with (");
					writer.append(itemName);
					writer.append(") where (");
					predicate.ToDialect(writer);
					writer.append(")");
					break;
			}
		}

		public IType check(Context context)
		{
			IType listType = source.check(context);
			if (!(listType is ContainerType))
				throw new SyntaxError("Expecting a list type as data source !");
			Context local = context.newLocalContext();
			IType itemType = ((ContainerType)listType).GetItemType();
			local.registerValue(new Variable(itemName, itemType));
			IType filterType = predicate.check(local);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError("Filtering expresion must return a bool !");
			return new ListType(itemType);
		}

		public IValue interpret(Context context)
		{
			IValue value = source.interpret(context);
			if (value == null)
				throw new NullReferenceError();
			if (!(value is IFilterable))
				throw new InternalError("Illegal fetch source: " + source);
			IFilterable list = (IFilterable)value;
			IType listType = source.check(context);
			if (!(listType is ContainerType))
				throw new InternalError("Illegal source type: " + listType.GetTypeName());
			IType itemType = ((ContainerType)listType).GetItemType();
			Context local = context.newLocalContext();
			Variable item = new Variable(itemName, itemType);
			local.registerValue(item);
			return list.Filter(local, itemName, predicate);
		}
	}
}
