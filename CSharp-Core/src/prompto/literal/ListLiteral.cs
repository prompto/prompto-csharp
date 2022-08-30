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
		bool mutable = false;
		IType itemType = null;
		ExpressionList expressions = null;

		public ListLiteral (bool mutable)
			: base ("[]", new ListValue (MissingType.Instance, new List<IValue>(), mutable))
		{
			this.mutable = mutable;
		}

		public ListLiteral (ExpressionList expressions, bool mutable)
			: base ("[" + expressions.ToString () + "]", new ListValue (MissingType.Instance, new List<IValue>(), mutable))
		{
			this.expressions = expressions;
			this.mutable = mutable;
		}

		public ExpressionList Expressions {
			get { return expressions; }
		}


		public override IType check (Context context)
		{
			if (itemType == null) {
				if (expressions != null)
					itemType = TypeUtils.InferExpressionsType (context, expressions);
				else
					itemType = TypeUtils.InferValuesType (context, value);
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
				return new ListValue (itemType, list, mutable);
			} else
				return value;
		}

		private IValue interpretPromotion(IValue item) {
			if(item==null)
				return item;
			if(DecimalType.Instance==itemType && item.GetIType()==IntegerType.Instance)
				return new prompto.value.DecimalValue(((prompto.value.IntegerValue)item).DoubleValue);
			else if(TextType.Instance==itemType && item.GetIType()==CharacterType.Instance)
				return ((CharacterValue)item).AsText();
			else
				return item;
		}

		public override void ToDialect (CodeWriter writer)
		{
			if(mutable)
				writer.append ("mutable ");
			if (expressions != null) {
				writer.append ('[');
				expressions.toDialect (writer);
				writer.append (']');
			} else
				writer.append ("[]");
		}

	}
}