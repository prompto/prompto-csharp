using System;
using prompto.parser;
using prompto.runtime;
using prompto.utils;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.value;
using prompto.expression;


namespace prompto.argument
{

    public class AttributeArgument : BaseArgument, INamedArgument
    {

        public AttributeArgument(String name)
            : base(name)
        {
        }

        
        public override String getSignature(Dialect dialect)
        {
            return GetName();
        }

        
		public override void ToDialect(CodeWriter writer)
        {
			writer.append(GetName());
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
        }

        
        public override String getProto()
        {
            return GetName();
        }

        
        public override bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is AttributeArgument))
                return false;
            AttributeArgument other = (AttributeArgument)obj;
			return ObjectUtils.AreEqual(this.GetName(), other.GetName());
        }

        
        public override void register(Context context)
        {
            context.registerValue(this, true);
			if (DefaultValue != null) {
				IValue value = DefaultValue.interpret (context);
				context.setValue (name, value);
			}
        }

        
        public override void check(Context context)
        {
            AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(name);
            if (actual == null)
                throw new SyntaxError("Unknown attribute: \"" + name + "\"");
        }

        
        public override IType GetIType(Context context)
        {
            IDeclaration named = context.getRegisteredDeclaration<IDeclaration>(name);
            return named.GetIType(context);
        }

        override
        public IValue checkValue(Context context, IExpression expression)
        {
            AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(name);
			return actual.checkValue(context, expression);
        }

    }
}
