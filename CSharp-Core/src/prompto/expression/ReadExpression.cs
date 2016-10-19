using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class ReadExpression : IExpression
    {

        IExpression resource;

        public ReadExpression(IExpression resource)
        {
            this.resource = resource;
        }


        public void ToDialect(CodeWriter writer)
        {
			writer.append("read from ");
			resource.ToDialect(writer);
        }

        public IType check(Context context)
        {
			context = context is ResourceContext ? context : context.newResourceContext();
			IType sourceType = resource.check(context);
            if (!(sourceType is ResourceType))
                throw new SyntaxError("Not a readable resource!");
            return TextType.Instance;
        }

		public IValue interpret(Context context)
        {
			Context resContext = context is ResourceContext ? context : context.newResourceContext();
			IValue o = resource.interpret(resContext);
            if (o == null)
                throw new NullReferenceError();
            if (!(o is IResource))
                throw new InternalError("Illegal read source: " + o);
            IResource res = (IResource)o;
            if (!res.isReadable())
                throw new InvalidResourceError("Not readable");
			try {
				if (context == resContext)
					return new Text(res.readLine());
				else
					return new Text (res.readFully());
			} finally {
				if (resContext != context)
					res.close ();
			}
        }
    }

}
