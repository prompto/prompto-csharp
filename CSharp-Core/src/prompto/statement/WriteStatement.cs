using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.expression;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.grammar;

namespace prompto.statement
{

	public class WriteStatement : SimpleStatement
	{

		IExpression content;
		IExpression resource;
		ThenWith thenWith;

		public WriteStatement (IExpression content, IExpression resource, ThenWith thenWith)
		{
			this.content = content;
			this.resource = resource;
			this.thenWith = thenWith;
		}

		public override bool IsSimple
		{
			get
            {
				return thenWith == null;
			}
			
		}

        public override void ToDialect (CodeWriter writer)
		{
			writer.append("write ");
			switch(writer.getDialect()) {
			case Dialect.E:
			case Dialect.M:
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
			if (thenWith != null)
				thenWith.ToDialect(writer, TextType.Instance);
		}

		 
		public override IType check (Context context)
		{
			context = context is ResourceContext ? context : context.newResourceContext();
			IType resourceType = resource.check (context);
			if (!(resourceType is ResourceType))
				throw new SyntaxError ("Not a resource!");
			if (thenWith != null)
				return thenWith.check(context, TextType.Instance);
			else
				return VoidType.Instance;
		}

		 
		public override IValue interpret (Context context)
		{
			Context resContext = context is ResourceContext ? context : context.newResourceContext();
			IValue o = resource.interpret (resContext);
			if (o == null)
				throw new NullReferenceError ();
			if (!(o is IResource))
				throw new InternalError ("Illegal write source: " + o);
			IResource res = (IResource)o;
			if (!res.isWritable ())
				throw new InvalidResourceError ("Not writable");
			try {
				String data = content.interpret(context).ToString();
				if(context==resContext)
				   res.writeLine(data);
				else if(thenWith != null)
					res.writeFully(data, text => {
						Context local = context.newChildContext();
						local.registerValue(new Variable(thenWith.Name, TextType.Instance));
						local.setValue(thenWith.Name, new TextValue(text));
						thenWith.Statements.interpret(local);
					});
				else
					res.writeFully (data);
				return null;
			} finally {
				if (resContext != context)
					res.close ();
			}
		}
	}

}