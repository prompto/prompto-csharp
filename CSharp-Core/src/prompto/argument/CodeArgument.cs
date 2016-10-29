using System;
using prompto.runtime;
using prompto.parser;
using prompto.utils;
using prompto.error;
using prompto.type;
using prompto.grammar;

namespace prompto.argument
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
			return name + ':' + CodeType.Instance.GetTypeName();
        }

        override
        public String getProto(Context context)
        {
			return CodeType.Instance.GetTypeName();
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			writer.append(CodeType.Instance.GetTypeName());
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
			return ObjectUtils.equal(this.GetName(), other.GetName());
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
        public IType GetIType(Context context)
        {
            return CodeType.Instance;
        }

    }

}
