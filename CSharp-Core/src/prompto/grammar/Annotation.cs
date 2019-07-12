using System;
using prompto.declaration;
using prompto.expression;
using prompto.literal;
using prompto.processor;
using prompto.runtime;
using prompto.utils;

namespace prompto.grammar
{
	public class Annotation
	{
		string name;
		DictEntryList arguments;

		public Annotation(string name, DictEntryList arguments)
		{
			this.name = name;
           	this.arguments = arguments;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(name);
			if (arguments != null && arguments.Count > 0)
			{
				writer.append("(");
				foreach (DictEntry entry in arguments)
				{
					if (entry.getKey() != null)
					{
						entry.getKey().ToDialect(writer);
						writer.append(" = ");
					}
					entry.getValue().ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(", ".Length);
				writer.append(")");
			}
			writer.newLine();
		}

		public void processCategory(Context context, CategoryDeclaration declaration)
		{
			AnnotationProcessor processor = AnnotationProcessor.forName(name);
			if (processor != null)
				processor.processCategory(this, context, declaration);
		}


		public object GetArgument(string name)
		{
			if (arguments != null)
			{
				foreach (DictEntry argument in arguments)
				{
					string key = (string)argument.getKey().asText().GetStorableData();
					if (key == name)
						return argument.getValue();
				}
			}
			return null;
		}
	}
}
