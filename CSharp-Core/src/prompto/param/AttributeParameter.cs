using System;
using prompto.parser;
using prompto.runtime;
using prompto.utils;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.value;
using prompto.expression;


namespace prompto.param
{

    public class AttributeParameter : BaseParameter
    {

        public AttributeParameter(String name)
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
            if (!(obj is AttributeParameter))
                return false;
            AttributeParameter other = (AttributeParameter)obj;
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

        
        public override IType check(Context context)
        {
            AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(name);
            if (actual == null)
                throw new SyntaxError("Unknown attribute: \"" + name + "\"");
            return actual.GetIType(context);
        }

        
        public override IType GetIType(Context context)
        {
            return check(context);
        }

        override
        public IValue checkValue(Context context, IExpression expression)
        {
            AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(name);
			return actual.checkValue(context, expression);
        }

    }
}
