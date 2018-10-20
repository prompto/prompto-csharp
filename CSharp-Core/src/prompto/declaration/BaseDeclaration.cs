using System;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using System.Collections.Generic;
using prompto.statement;
using prompto.grammar;

namespace prompto.declaration
{

	public abstract class BaseDeclaration : Section, IDeclaration
	{
		List<CommentStatement> comments = null;
		List<Annotation> annotations = null;

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

		public List<Annotation> Annotations
		{
			get
			{
				return annotations;
			}
			set
			{
				annotations = value;
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

		public abstract IType GetIType (Context context);

		public abstract void register (Context context);

		public abstract void ToDialect (CodeWriter writer);

		public IMethodDeclaration ClosureOf {
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

	}

}