using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class ReadOneExpression : IExpression
    {

        IExpression resource;

        public ReadOneExpression(IExpression resource)
        {
            this.resource = resource;
        }


        public void ToDialect(CodeWriter writer)
        {
			writer.append("read one from ");
			resource.ToDialect(writer);
        }

        public IType check(Context context)
        {
			if(!(context is ResourceContext))
				throw new SyntaxError("Not a resource context!");
			IType sourceType = resource.check(context);
            if (!(sourceType is ResourceType))
                throw new SyntaxError("Not a readable resource!");
            return TextType.Instance;
        }

		public IValue interpret(Context context)
		{
			if (!(context is ResourceContext))
				throw new SyntaxError("Not a resource context!");
			IValue o = resource.interpret(context);
			if (o == null)
				throw new NullReferenceError();
			if (!(o is IResource))
				throw new InternalError("Illegal read source: " + o);
			IResource res = (IResource)o;
			if (!res.isReadable())
				throw new InvalidResourceError("Not readable");
			return new Text(res.readLine());
		}
    }

}
