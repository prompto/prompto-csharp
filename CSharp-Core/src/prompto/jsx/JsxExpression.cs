using prompto.declaration;
using prompto.error;
using prompto.expression;
using prompto.literal;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public class JsxExpression : IJsxValue, IJsxExpression
	{

		IExpression expression;

		public JsxExpression(IExpression expression)
		{
			this.expression = expression;
		}

		public IType check(Context context)
		{
			return expression.check(context);
		}

		public virtual IType checkReference(Context context)
		{
			return check(context);
		}

		public AttributeDeclaration CheckAttribute(Context context)
		{
			throw new SyntaxError("Expected an attribute, found: " + this.ToString());
		}

		public IType checkProto(Context context, MethodType type)
		{
			if (expression is ArrowExpression)
				return type.checkArrowExpression(context, (ArrowExpression)expression);
			else if (expression is TypeLiteral)
				return ((TypeLiteral)expression).getType().Resolve(context);
			else
				return expression.check(context);
		}

		public bool IsLiteral()
		{
			return expression.GetType().GetGenericTypeDefinition()==typeof(Literal<>);
		}


		public IValue interpret(Context context)
		{
			return new JsxValue(this);
		}

		public virtual IValue interpretReference(Context context)
		{
			return interpret(context);
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append("{");
			expression.ToDialect(writer);
			writer.append("}");
		}

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }

      }

}