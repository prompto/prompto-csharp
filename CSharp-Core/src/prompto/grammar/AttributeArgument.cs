using System;
using prompto.parser;
using prompto.runtime;
using prompto.utils;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.value;
using prompto.expression;


namespace prompto.grammar
{

    public class AttributeArgument : BaseArgument, INamedArgument
    {

        public AttributeArgument(String name)
            : base(name)
        {
        }

        override
        public String getSignature(Dialect dialect)
        {
            return GetName();
        }

        override
		public void ToDialect(CodeWriter writer)
        {
			writer.append(GetName());
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
        }

        override
        public String getProto(Context context)
        {
            return GetName();
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is AttributeArgument))
                return false;
            AttributeArgument other = (AttributeArgument)obj;
			return Utils.equal(this.GetName(), other.GetName());
        }

        override
        public void register(Context context)
        {
            context.registerValue(this, true);
			if (DefaultValue != null) {
				IValue value = DefaultValue.interpret (context);
				context.setValue (name, value);
			}
        }

        override
        public void check(Context context)
        {
            AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(name);
            if (actual == null)
                throw new SyntaxError("Unknown attribute: \"" + name + "\"");
        }

        override
        public IType GetType(Context context)
        {
            IDeclaration named = context.getRegisteredDeclaration<IDeclaration>(name);
            return named.GetType(context);
        }

        override
        public IValue checkValue(Context context, IExpression expression)
        {
            AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(name);
			return actual.checkValue(context, expression);
        }

    }
}
