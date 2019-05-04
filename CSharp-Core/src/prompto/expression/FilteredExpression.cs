using prompto.parser;
using System;
using prompto.runtime;
using prompto.error;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.grammar;

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
			if (itemName != null)
				ToDialectExplicit(writer);
			else if (predicate is ArrowExpression)
				((ArrowExpression)predicate).FilterToDialect(writer, source);
			else
				throw new SyntaxError("Expecting an arrow expression!");
		}


		public void ToDialectExplicit(CodeWriter writer)
		{
			writer = writer.newChildWriter();
			IType sourceType = source.check(writer.getContext());
			IType itemType = ((IterableType)sourceType).GetItemType();
			writer.getContext().registerValue(new Variable(itemName, itemType));
			switch (writer.getDialect())
			{
				case Dialect.E:
				case Dialect.M:
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
			IType sourceType = source.check(context);
			if (!(sourceType is ContainerType))
				throw new SyntaxError("Expecting a list type as data source !");
			IType itemType = ((ContainerType)sourceType).GetItemType();
			if (itemName != null)
			{
				Context child = context.newChildContext();
				child.registerValue(new Variable(itemName, itemType));
				IType filterType = predicate.check(child);
				if (filterType != BooleanType.Instance)
					throw new SyntaxError("Filtering expresion must return a bool !");
			}
			else if (predicate is ArrowExpression)
			{
				// TODO
			}
			else
				throw new SyntaxError("Expecting an arrow expression!");
			return sourceType;
		}

		public IValue interpret(Context context)
		{
			IType sourceType = source.check(context);
			if (!(sourceType is ContainerType))
				throw new InternalError("Illegal source type: " + sourceType.GetTypeName());
			IType itemType = ((ContainerType)sourceType).GetItemType();
			IValue value = source.interpret(context);
			if (value == null)
				throw new NullReferenceError();
			if (!(value is IFilterable))
				throw new InternalError("Illegal fetch source: " + source);
			IFilterable list = (IFilterable)value;
			ArrowExpression arrow = toArrowExpression();
			Predicate<IValue> filter = arrow.GetFilter(context, itemType);
			return list.Filter(filter);
		}

		ArrowExpression toArrowExpression()
		{
			if (itemName != null)
			{
				ArrowExpression arrow = new ArrowExpression(new IdentifierList(itemName), null, null);
				arrow.Expression = predicate;
				return arrow;
			}
			else if (predicate is ArrowExpression)
				return (ArrowExpression)predicate;
			else
				throw new SyntaxError("Not a valid filter!");
		}
	}
}

