using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.expression;
using prompto.type;
using prompto.value;
using prompto.utils;

namespace prompto.statement
{

	public class WriteStatement : SimpleStatement
	{

		IExpression content;
		IExpression resource;

		public WriteStatement (IExpression content, IExpression resource)
		{
			this.content = content;
			this.resource = resource;
		}


		override 
		public void ToDialect (CodeWriter writer)
		{
			writer.append("write ");
			switch(writer.getDialect()) {
			case Dialect.E:
			case Dialect.S:
				content.ToDialect(writer);
				break;
			case Dialect.O:
				writer.append("(");
				content.ToDialect(writer);
				writer.append(")");
				break;
			}
			writer.append(" to ");
			resource.ToDialect(writer);
		}

		override 
		public IType check (Context context)
		{
			context = context.newResourceContext ();
			IType resourceType = resource.check (context);
			if (!(resourceType is ResourceType))
				throw new SyntaxError ("Not a resource!");
			return VoidType.Instance;
		}

		override 
		public IValue interpret (Context context)
		{
			context = context.newResourceContext ();
			IValue o = resource.interpret (context);
			if (o == null)
				throw new NullReferenceError ();
			if (!(o is IResource))
				throw new InternalError ("Illegal write source: " + o);
			IResource res = (IResource)o;
			if (!res.isWritable ())
				throw new InvalidResourceError ("Not writable");
			res.writeFully (content.interpret (context).ToString ());
			return null;
		}
	}

}