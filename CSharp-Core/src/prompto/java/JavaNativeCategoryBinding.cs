using prompto.grammar;
using prompto.utils;

namespace prompto.java
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
