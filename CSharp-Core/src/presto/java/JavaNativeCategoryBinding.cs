using presto.grammar;
using presto.utils;

namespace presto.java
{

    public class JavaNativeCategoryBinding : NativeCategoryBinding
    {

        JavaIdentifierExpression expression;

        public JavaNativeCategoryBinding(JavaIdentifierExpression expression)
        {
            this.expression = expression;
        }

        public JavaIdentifierExpression getExpression()
        {
            return expression;
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Java: ");
			expression.ToDialect(writer);
		}

    }
}
