using System;
using presto.runtime;
using presto.parser;
using presto.type;
using presto.utils;
using presto.grammar;
using presto.value;


namespace presto.expression
{

public interface IExpression : IDialectElement {
	
	IType check(Context context);
	IValue interpret(Context context);
	
}

}