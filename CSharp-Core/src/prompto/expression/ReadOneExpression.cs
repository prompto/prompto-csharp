using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class ReadOneExpression : BaseExpression, IExpression
    {

        IExpression resource;

        public ReadOneExpression(IExpression resource)
        {
            this.resource = resource;
        }


        public override void ToDialect(CodeWriter writer)
        {
			writer.append("read one from ");
			resource.ToDialect(writer);
        }

        public override IType check(Context context)
        {
			if(!(context.IsInResourceContext))
				throw new SyntaxError("Not a resource context!");
			IType sourceType = resource.check(context);
            if (!(sourceType is ResourceType))
                throw new SyntaxError("Not a readable resource!");
            return TextType.Instance;
        }

		public override IValue interpret(Context context)
		{
			if (!(context.IsInResourceContext))
				throw new SyntaxError("Not a resource context!");
			IValue o = resource.interpret(context);
			if (o == null)
				throw new NullReferenceError();
			if (!(o is IResource))
				throw new InternalError("Illegal read source: " + o);
			IResource res = (IResource)o;
			if (!res.isReadable())
				throw new InvalidResourceError("Not readable");
			String line = res.readLine();
			if (line == null)
				return NullValue.Instance;
			else
				return new TextValue(line);
		}
    }

}
