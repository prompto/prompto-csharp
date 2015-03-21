using presto.value;
using presto.type;
using presto.utils;
using presto.runtime;
using System.Collections.Generic;
using presto.expression;

namespace presto.literal
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
				itemType = Utils.InferElementType(context, expressions);
			else
				itemType = Utils.InferElementType(context, value.getItems());
		}
		return new SetType(itemType); 
	}

		public override IValue interpret(Context context) {
		if(expressions!=null) {
			check(context); // force computation of itemType
			HashSet<IValue> set = new HashSet<IValue>();
			foreach(IExpression exp in expressions)
				set.Add(exp.interpret(context));
			if(itemType==null)
				itemType = Utils.InferElementType(context, set); 
			value = new SetValue(itemType, set);
			expressions = null;
		}
		return value;
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
