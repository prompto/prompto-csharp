using presto.error;
using presto.runtime;
using System;
using presto.parser;
using presto.expression;
using presto.type;
using presto.value;
using presto.utils;

namespace presto.statement
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
			case Dialect.P:
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