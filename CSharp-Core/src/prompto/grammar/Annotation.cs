using System;
using prompto.expression;
using prompto.utils;

namespace prompto.grammar
{
	public class Annotation
	{
		IExpression expression;
		string name;

		public Annotation(string name, IExpression expression)
		{
			this.name = name;
           	this.expression = expression;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(name);
			if (expression != null)
			{
				writer.append("(");
				expression.ToDialect(writer);
				writer.append(")");
			}
			writer.newLine();
		}

	}
}
