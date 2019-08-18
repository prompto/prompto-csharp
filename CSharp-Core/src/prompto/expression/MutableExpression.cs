using System;
using prompto.error;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{

	public class MutableExpression : BaseExpression, IExpression
	{
		IExpression source;

		public MutableExpression(IExpression source)
		{
			this.source = source;
		}


		public override IType check(Context context)
		{
			IType sourceType = source.check(context);
			if(!(sourceType is CategoryType))
				 throw new SyntaxError("Expected a category instance, got:" + sourceType);
			return new CategoryType((CategoryType)sourceType, true);
		}

		public override IValue interpret(Context context)
		{
			CategoryType type = (CategoryType)check(context);
			ConstructorExpression ctor = new ConstructorExpression(type, source, null, true);
			return ctor.interpret(context);
		}

		public override void ToDialect(CodeWriter writer)
		{
			writer.append("mutable ");
			source.ToDialect(writer);
		}
	}
}
