using System;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.type;
using presto.value;
using presto.utils;


namespace presto.expression
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
            context = context.newResourceContext();
            IType sourceType = resource.check(context);
            if (!(sourceType is ResourceType))
                throw new SyntaxError("Not a readable resource!");
            return TextType.Instance;
        }

		public IValue interpret(Context context)
        {
            context = context.newResourceContext();
			IValue o = resource.interpret(context);
            if (o == null)
                throw new NullReferenceError();
            if (!(o is IResource))
                throw new InternalError("Illegal read source: " + o);
            IResource res = (IResource)o;
            if (!res.isReadable())
                throw new InvalidResourceError("Not readable");
			String str = res.readFully();
			return new Text (str);
        }
    }

}
