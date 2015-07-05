using System;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.grammar;
using prompto.value;


namespace prompto.expression
{

public interface IExpression : IDialectElement {
	
	IType check(Context context);
	IValue interpret(Context context);
	
}

}