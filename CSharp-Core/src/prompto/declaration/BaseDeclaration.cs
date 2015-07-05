using System;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;

namespace prompto.declaration
{

	public abstract class BaseDeclaration : Section, IDeclaration
	{

		protected String name;

		protected BaseDeclaration (String name)
		{
			this.name = name;
		}

		public String GetName ()
		{
			return name;
		}

		public override int GetHashCode ()
		{
			return name.GetHashCode ();
		}

		public abstract IType check (Context context);

		public abstract IType GetType (Context context);

		public abstract void register (Context context);

		public abstract void ToDialect (CodeWriter writer);
	}

}