using prompto.runtime;
using System;
using prompto.error;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.utils;
using System.Collections.Generic;

namespace prompto.literal
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
				check(context); // force computation of itemType
				List<IValue> list = new List<IValue> ();
				foreach (IExpression exp in expressions) 
				{
					IValue item = exp.interpret (context);
					item = interpretPromotion (item);
					list.add (item);
				}
				value = new ListValue (itemType, list);
				// don't dispose of expressions, they are required by translation 
			}
			return value;
		}

		private IValue interpretPromotion(IValue item) {
			if(item==null)
				return item;
			if(DecimalType.Instance==itemType && item.GetIType()==IntegerType.Instance)
				return new prompto.value.Decimal(((prompto.value.Integer)item).DecimalValue);
			else if(TextType.Instance==itemType && item.GetIType()==CharacterType.Instance)
				return ((Character)item).AsText();
			else
				return item;
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