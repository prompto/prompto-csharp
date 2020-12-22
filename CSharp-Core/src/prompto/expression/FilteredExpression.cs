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

	public class FilteredExpression : BaseExpression, IExpression
	{

		IExpression source;
		PredicateExpression predicate;

		public FilteredExpression(IExpression source, PredicateExpression predicate)
		{
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


		public override void ToDialect(CodeWriter writer)
		{
			predicate.FilteredToDialect(writer, source);
		}


		public override IType check(Context context)
		{
			IType sourceType = source.check(context);
			if (!(sourceType is IterableType))
				throw new SyntaxError("Expecting an iterable type as data source!");
			IType itemType = ((IterableType)sourceType).GetItemType();
			ArrowExpression arrow = predicate.ToArrowExpression();
			IType filterType = arrow.CheckFilter(context, itemType);
			if (filterType != BooleanType.Instance)
				throw new SyntaxError("Filtering expresion must return a bool!");
			return new ListType(itemType);
		}

		public override IValue interpret(Context context)
		{
			IType sourceType = source.check(context);
			if (!(sourceType is IterableType))
				throw new InternalError("Illegal source type: " + sourceType.GetTypeName());
			IType itemType = ((IterableType)sourceType).GetItemType();
			IValue value = source.interpret(context);
			if (value == null)
				throw new NullReferenceError();
			if (!(value is IFilterable))
				throw new InternalError("Illegal fetch source: " + source);
			IFilterable list = (IFilterable)value;
			ArrowExpression arrow = predicate.ToArrowExpression();
			Predicate<IValue> filter = arrow.GetFilter(context, itemType);
			return list.Filter(filter);
		}

	}
}

