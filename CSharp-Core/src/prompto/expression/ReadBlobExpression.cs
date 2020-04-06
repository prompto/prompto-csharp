using prompto.runtime;
using prompto.error;
using prompto.type;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class ReadBlobExpression : BaseExpression, IExpression
    {

        IExpression resource;

        public ReadBlobExpression(IExpression resource)
        {
            this.resource = resource;
        }


        public override void ToDialect(CodeWriter writer)
        {
			writer.append("read Blob from ");
			resource.ToDialect(writer);
        }

        public override IType check(Context context)
        {
			context = context.newResourceContext();
			IType sourceType = resource.check(context);
            if (!(sourceType is ResourceType))
                throw new SyntaxError("Not a readable resource!");
            return BlobType.Instance;
        }

		public override IValue interpret(Context context)
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
			try {
				return new BlobValue(res.readBinary());
			} finally {
				res.close ();
			}
        }
    }

}
