using System;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using System.Collections.Generic;
using prompto.statement;

namespace prompto.declaration
{

	public abstract class BaseDeclaration : Section, IDeclaration
	{
		List<CommentStatement> comments = null;

 		protected String name;

		protected BaseDeclaration (String name)
		{
			this.name = name;
		}

		public List<CommentStatement> Comments {
			get {
				return comments;
			}
			set {
				comments = value;
			}
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