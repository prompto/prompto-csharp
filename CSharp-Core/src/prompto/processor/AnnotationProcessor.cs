using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.grammar;
using prompto.runtime;

namespace prompto.processor
{
	public abstract class AnnotationProcessor
	{
		static Dictionary<string, AnnotationProcessor> processors = new Dictionary<String, AnnotationProcessor>();

		public static AnnotationProcessor forName(string name)
		{
				AnnotationProcessor result;

				if (processors.TryGetValue(name, out result))
					return result;
				String simpleName = name.Substring(1) + "Processor";
				String fullName = typeof(AnnotationProcessor).Namespace + "." + simpleName;
				try 
				{
					Type klass = Type.GetType(fullName);
					result = (AnnotationProcessor)Activator.CreateInstance(klass);
					return result;
				} catch(Exception) {
					return null;
				} 
			}

		public abstract void processCategory(Annotation annotation, Context context, CategoryDeclaration declaration);
	}
}
