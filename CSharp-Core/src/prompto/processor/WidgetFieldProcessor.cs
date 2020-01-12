using System;
using prompto.declaration;
using prompto.error;
using prompto.expression;
using prompto.grammar;
using prompto.literal;
using prompto.runtime;
using prompto.type;

namespace prompto.processor
{
	public class WidgetFieldProcessor : AnnotationProcessor
	{

		public override void ProcessCategory(Annotation annotation, Context context, CategoryDeclaration declaration)
		{
			if (declaration.IsAWidget(context))
				doProcessCategory(annotation, context, declaration);
			else
				throw new SyntaxError("WidgetField is only applicable to widgets");
		}

		private void doProcessCategory(Annotation annotation, Context context, CategoryDeclaration declaration)
		{
			Object fieldName = annotation.GetArgument("name");
			Object fieldType = annotation.GetArgument("type");
			if (!(fieldName is TextLiteral))
				throw new SyntaxError("WidgetField requires a Text value for argument 'name'");
			else if (!(fieldType is TypeLiteral || fieldType is TypeExpression))
				throw new SyntaxError("WidgetField requires a Type value for argument 'type'");
			else 
			{
				String name = ((TextLiteral)fieldName).ToString();
				IType type = fieldType is TypeLiteral ? ((TypeLiteral)fieldType).getType() : ((TypeExpression)fieldType).getType();
				context.registerValue(new Variable(name.Substring(1, name.Length - 2), type), false);
			}
		}
	}

}
