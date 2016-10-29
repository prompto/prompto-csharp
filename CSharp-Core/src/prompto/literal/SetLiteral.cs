using prompto.value;
using prompto.type;
using prompto.utils;
using prompto.runtime;
using System.Collections.Generic;
using prompto.expression;

namespace prompto.literal
{

public class SetLiteral : Literal<SetValue> {

	IType itemType = null;
	ExpressionList expressions;

	public SetLiteral()
		: base("<>", new SetValue(MissingType.Instance))
		{
	}

	public SetLiteral(ExpressionList expressions)
		: base("<" + expressions.ToString() + ">", new SetValue(MissingType.Instance))
		{
			this.expressions = expressions;
	}

	public override IType check(Context context) {
		if(itemType==null) {
			if(expressions!=null)
				itemType = TypeUtils.InferElementType(context, expressions);
			else
					itemType = TypeUtils.InferElementType(context, value.getItems());
		}
		return new SetType(itemType); 
	}

	public override IValue interpret(Context context) {
		if(expressions!=null) {
			check(context); // force computation of itemType
			HashSet<IValue> set = new HashSet<IValue>();
			foreach (IExpression exp in expressions) {
				IValue item = exp.interpret (context);
				item = interpretPromotion (item);
				set.Add (item);
			}; 
			return new SetValue(itemType, set);
		} else
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

	public void toDialect(CodeWriter writer) {
		if(expressions!=null) {
			writer.append('<');
			expressions.toDialect(writer);
			writer.append('>');
		} else
			writer.append("< >");
	}


}
}
