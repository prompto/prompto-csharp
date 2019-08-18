using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{

	public class CodeExpression : BaseExpression, IExpression
	{

		IExpression expression;

		public CodeExpression (IExpression expression)
		{
			this.expression = expression;
		}

		public override void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				writer.append ("Code: ");
				expression.ToDialect (writer);
				break;
			case Dialect.O:
			case Dialect.M:
				writer.append ("Code(");
				expression.ToDialect (writer);
				writer.append (")");
				break;
			}
		}

		public override IType check (Context context)
		{
			return CodeType.Instance;
		}

		public override IValue interpret (Context context)
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