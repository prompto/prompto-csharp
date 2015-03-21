using System;
using presto.runtime;
using presto.parser;
using presto.utils;
using presto.error;
using presto.type;

namespace presto.grammar
{

    public class CodeArgument : BaseArgument, ITypedArgument
    {

        public CodeArgument(String name)
            : base(name)
        {
        }

        public IType getType()
        {
            return CodeType.Instance;
        }

        override
        public String getSignature(Dialect dialect)
        {
            return name + ':' + CodeType.Instance.getName();
        }

        override
        public String getProto(Context context)
        {
            return CodeType.Instance.getName();
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			writer.append(CodeType.Instance.getName());
			writer.append(" ");
			writer.append(name);
       }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is CodeArgument))
                return false;
            CodeArgument other = (CodeArgument)obj;
            return Utils.equal(this.getName(), other.getName());
        }

        override
        public void register(Context context)
        {
			INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual != null)
                throw new SyntaxError("Duplicate argument: \"" + name + "\"");
            context.registerValue(this);
        }

        override
        public void check(Context context)
        {
        }

        override
        public IType GetType(Context context)
        {
            return CodeType.Instance;
        }

    }

}
