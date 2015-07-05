using prompto.runtime;
using System;
using prompto.error;
using prompto.value;
using prompto.expression;
using prompto.utils;

namespace prompto.grammar
{

	public class MatchingCollectionConstraint : IAttributeConstraint
	{
	
		IExpression collection;

		public MatchingCollectionConstraint (IExpression collection)
		{
			this.collection = collection;
		}

		public void checkValue (Context context, IValue value)
		{
			IValue container = collection.interpret (context);
			if (container is IContainer) {
				if (!((IContainer)container).HasItem (context, value))
					throw new InvalidDataError ("Value:" + value.ToString () + " is not in: " + collection.ToString ());
			} else
				throw new InvalidDataError ("Not a container: " + collection.ToString ());
		}

		public void ToDialect (CodeWriter writer)
		{
			writer.append (" in ");
			collection.ToDialect (writer);
		}

	}

}