using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.expression
{

	public class CodeExpression : IExpression
	{

		IExpression expression;

		public CodeExpression (IExpression expression)
		{
			this.expression = expression;
		}

		public void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				writer.append ("Code: ");
				expression.ToDialect (writer);
				break;
			case Dialect.O:
			case Dialect.P:
				writer.append ("Code(");
				expression.ToDialect (writer);
				writer.append (")");
				break;
			}
		}

		public IType check (Context context)
		{
			return CodeType.Instance;
		}

		public IValue interpret (Context context)
		{
			return new CodeValue(this);
		}
	
		// expression can only be checked and evaluated in the context of an execute:

		public IType checkCode (Context context)
		{
			return expression.check (context);
		}

		public IValue interpretCode (Context context)
		{
			return expression.interpret (context);
		}
	
	}

}