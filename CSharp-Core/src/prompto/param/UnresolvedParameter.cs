using prompto.runtime;
using System;
using prompto.parser;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.utils;
using prompto.expression;
using prompto.value;
using prompto.grammar;

namespace prompto.param
{

    public class UnresolvedParameter : BaseParameter, IDialectElement
    {

        IParameter resolved = null;

        public UnresolvedParameter(String name)
			: base(name)
        {
        }

        public override string ToString()
        {
            return GetName();
        }

        public override String getSignature(Dialect dialect)
        {
			return GetName();
        }
			
		public override void ToDialect(CodeWriter writer)
        {
			writer.append(name);
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
       }

		public override IType check(Context context)
        {
            return resolveAndCheck(context);
        }

		public override String getProto()
        {
            return name;
        }

		public override IType GetIType(Context context)
        {
            return resolveAndCheck(context);
        }

		public override void register(Context context)
        {
            resolveAndCheck(context);
            resolved.register(context);
        }

		public override IValue checkValue(Context context, IExpression expression)
        {
            resolveAndCheck(context);
			return resolved.checkValue(context, expression);
        }

        private IType resolveAndCheck(Context context)
        {
            resolve(context);
            return resolved.GetIType(context);
        }

        private void resolve(Context context) {
            if (resolved != null)
                return;
            IDeclaration named = context.getRegisteredDeclaration<IDeclaration>(name);
            if (named is AttributeDeclaration)
                resolved = new AttributeParameter(name);
            else if (named is MethodDeclarationMap)
                resolved = new MethodParameter(name);
            if (resolved != null)
                resolved.setMutable(this.isMutable());
            else
                throw new SyntaxError("Unknown identifier:" + name);
        }

    }

}