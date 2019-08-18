using System;
using prompto.type;
using prompto.runtime;
using prompto.error;
using prompto.utils;
using prompto.parser;
using prompto.value;
using prompto.declaration;

namespace prompto.expression {

	public class CastExpression : BaseExpression, IExpression {

	public static IType anyfy(IType type)
	{
		if (type is CategoryType && "Any".Equals(((CategoryType)type).GetTypeName()))
			return AnyType.Instance;	
		else
			return type;
	}

		IExpression expression;
		IType type;

		public CastExpression(IExpression expression, IType type) {
			this.expression = expression;
			this.type = type;
		}

		public override IType check(Context context) {
			IType actual = anyfy(expression.check(context));
			IType target = getTargetType(context, type);
			// check any
			if (actual == AnyType.Instance)
				return target;
			// check upcast
			if (target.isAssignableFrom(context, actual))
				return target;
			// check downcast
			if(actual.isAssignableFrom(context, target))
				return type;
			throw new SyntaxError("Cannot cast " + actual.ToString() + " to " + target.ToString());
		}

		private static IType getTargetType(Context context, IType type)
		{
			if (type is IterableType)
			{
				IType itemType = getTargetType(context, ((IterableType)type).GetItemType());
				return ((IterableType)type).WithItemType(itemType);
			}
			else if (type is NativeType)
				return type;
			else
				return getTargetAtomicType(context, type);
		}


		private static IType getTargetAtomicType(Context context, IType type)
		{
			IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(type.GetTypeName());
			if (decl == null)
				throw new SyntaxError("Unknown identifier: " + type.GetTypeName());
			else if(decl is MethodDeclarationMap) {
				MethodDeclarationMap map = (MethodDeclarationMap)decl;
				if(map.Count==1)
					return new MethodType(map.GetFirst());
				else
					throw new SyntaxError("Ambiguous identifier: " + type.GetTypeName());
			} else
				return decl.GetIType(context);
		}


		public override IValue interpret(Context context) {
			IValue value = expression.interpret(context);
			if (value != null)
			{
				IType target = getTargetType(context, type);
				if (target == DecimalType.Instance && value is Integer)
					value = new value.Decimal(((Integer)value).DecimalValue);
				else if (target == IntegerType.Instance && value is value.Decimal)
					value = new Integer(((value.Decimal)value).IntegerValue);
				else if (target.isMoreSpecificThan(context, value.GetIType()))
					value.SetIType(type);
			}
			return value;
		}

		public override void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
			case Dialect.M:
				expression.ToDialect(writer);
				writer.append(" as ");
				type.ToDialect(writer);
				break;
			case Dialect.O:
				writer.append("(");
				type.ToDialect(writer);
				writer.append(")");
				expression.ToDialect(writer);
				break;
			}

		}

	}

}