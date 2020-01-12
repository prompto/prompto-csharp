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
		String name;
		DictEntryList arguments;

		public Annotation(String name, DictEntryList arguments)
		{
			this.name = name;
			this.arguments = arguments;
		}

		public String Name
        {
            get
            {
                return name;
            }
        }

		public void ToDialect(CodeWriter writer)
		{
			writer.append(name);
			if (arguments != null && arguments.Count > 0)
			{
				writer.append("(");
				foreach (DictEntry entry in arguments)
				{
					if (entry.GetKey() != null)
					{
						entry.GetKey().ToDialect(writer);
						writer.append(" = ");
					}
					entry.GetValue().ToDialect(writer);
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
				processor.ProcessCategory(this, context, declaration);
		}


		public object GetArgument(string name)
		{
			if (arguments != null)
			{
				foreach (DictEntry argument in arguments)
				{
					string key = (string)argument.GetKey().asText().GetStorableData();
					if (key == name)
						return argument.GetValue();
				}
			}
			return null;
		}

		public object GetDefaultArgument()
        {
			if (arguments != null && arguments.Count == 1)
				return arguments[0].GetValue();
			else
				return null;

		}

			

	}
}
