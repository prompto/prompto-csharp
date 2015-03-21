using presto.runtime;
using System;
using presto.parser;
using presto.error;
using presto.type;
using presto.declaration;
using presto.utils;
using presto.expression;
using presto.value;

namespace presto.grammar
{

    public class UnresolvedArgument : BaseArgument, INamedArgument, IDialectElement
    {

        INamedArgument resolved = null;

        public UnresolvedArgument(String name)
			: base(name)
        {
        }

        public override String getSignature(Dialect dialect)
        {
            return getName();
        }
			
		public override void ToDialect(CodeWriter writer)
        {
			writer.append(name);
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
       }

		public override void check(Context context)
        {
            resolveAndCheck(context);
        }

		public override String getProto(Context context)
        {
            resolveAndCheck(context);
            return resolved.getProto(context);
        }

		public override IType GetType(Context context)
        {
            resolveAndCheck(context);
            return resolved.GetType(context);
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

        private void resolveAndCheck(Context context)
        {
            if (resolved != null)
                return;
            IDeclaration named = context.getRegisteredDeclaration<IDeclaration>(name);
            if (named is AttributeDeclaration)
                resolved = new AttributeArgument(name);
            else if (named is MethodDeclarationMap)
                resolved = new MethodArgument(name);
            else
                throw new SyntaxError("Unknown identifier:" + name);
        }

    }

}