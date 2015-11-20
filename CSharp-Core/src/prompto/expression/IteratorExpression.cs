using System;
using prompto.expression;
using prompto.type;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
using prompto.error;
using prompto.utils;
using prompto.parser;

namespace prompto.expression
{

public class IteratorExpression : IExpression {

	String name;
	IExpression source;
	IExpression expression;

	public IteratorExpression(String name, IExpression source, IExpression exp) {
		this.name = name;
		this.source = source;
		this.expression = exp;
	}

	public IType check(Context context) {
		IType srcType = source.check(context);
		IType elemType = srcType.checkIterator(context);
		Context child = context.newChildContext();
		context.registerValue(new Variable(name, elemType));
		IType itemType = expression.check(child);
		return new IteratorType(itemType);
	}

	public IValue interpret(Context context) {
		IType iterType = check(context);
			IType itemType = ((IteratorType)iterType).GetItemType();
		IValue items = source.interpret(context);
		Integer length = (Integer)items.GetMember(context, "length", false);
		IEnumerator<IValue> iterator = getEnumerator(context, items);
		return new Iterator(itemType, context, length, name, iterator, expression);
	}

	private IEnumerator<IValue> getEnumerator(Context context, Object src) {
		if (src is IIterable) 
			return ((IIterable) src).GetEnumerable(context).GetEnumerator();
			else if(src is IEnumerable<IValue>)
				return ((IEnumerable<IValue>)src).GetEnumerator();
		else
			throw new InternalError("Should never get there!");
	}

		public void ToDialect(CodeWriter writer) {
		switch(writer.getDialect()) {
		case Dialect.E:
			toEDialect(writer);
			break;
			case Dialect.O:
			toODialect(writer);
			break;
			case Dialect.S:
			toPDialect(writer);
			break;
		}
	}

	private void toPDialect(CodeWriter writer) {
		expression.ToDialect(writer);
		writer.append(" for ");
		writer.append(name.ToString());
		writer.append(" in ");
			source.ToDialect(writer);
	}

	private void toODialect(CodeWriter writer) {
			expression.ToDialect(writer);
		writer.append(" for each ( ");
			writer.append(name.ToString());
		writer.append(" in ");
			source.ToDialect(writer);
		writer.append(" )");
	}

	private void toEDialect(CodeWriter writer) {
			expression.ToDialect(writer);
		writer.append(" for each ");
			writer.append(name.ToString());
		writer.append(" in ");
			source.ToDialect(writer);
	}


}
}