using presto.runtime;
using System;
using presto.error;
using presto.value;
using presto.type;
using presto.expression;
using presto.utils;
using System.Collections.Generic;

namespace presto.literal
{

	public class ListLiteral : Literal<ListValue>
	{

		IType itemType = null;
		ExpressionList expressions = null;

		public ListLiteral ()
			: base ("[]", new ListValue (MissingType.Instance))
		{
		}

		public ListLiteral (ExpressionList expressions)
			: base ("[" + expressions.ToString () + "]", new ListValue (MissingType.Instance))
		{
			this.expressions = expressions;
		}

		public ExpressionList Expressions {
			get { return expressions; }
		}


		public override IType check (Context context)
		{
			if (itemType == null) {
				if (expressions != null)
					itemType = Utils.InferElementType (context, expressions);
				else
					itemType = Utils.InferElementType (context, value);
			}
			return new ListType (itemType);
		}


		public override IValue interpret (Context context)
		{
			if (expressions != null) {
				List<IValue> list = new List<IValue> ();
				foreach (IExpression exp in expressions)
					list.add (exp.interpret (context));
				if (itemType == null)
					itemType = Utils.InferElementType (context, list); 
				value = new ListValue (itemType, list);
				// don't dispose of expressions, they are required by translation 
			}
			return value;
		}


		public override void ToDialect (CodeWriter writer)
		{
			if (expressions != null) {
				writer.append ('[');
				expressions.toDialect (writer);
				writer.append (']');
			} else
				writer.append ("[]");
		}

	}
}