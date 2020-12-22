using prompto.runtime;
using prompto.error;
using System;
using System.Reflection;
using prompto.declaration;
using prompto.store;
using prompto.value;
using prompto.expression;

namespace prompto.type
{

    public class MethodType : BaseType
    {

		IMethodDeclaration method;

        public MethodType(IMethodDeclaration method)
			: base(TypeFamily.METHOD)
        {
			this.method = method;
        }

		public IMethodDeclaration Method
		{
			get
			{
				return method;
			}
		}

		public override string GetTypeName()
		{
			return method.GetName();
		}

       
        public override Type ToCSharpType(Context context)
        {
            return typeof(MethodInfo);
        }

        
        public override bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is MethodType))
                return false;
            return this.method.getProto().Equals(((MethodType)obj).method.getProto());
        }

        
        public override void checkUnique(Context context)
        {
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>(method.GetName());
            if (actual != null)
				throw new SyntaxError("Duplicate name: \"" + method.GetName() + "\"");
        }

        public override void checkExists(Context context)
        {
            // nothing to do
        }


        public override bool isMoreSpecificThan(Context context, IType other)
        {
            // TODO Auto-generated method stub
            return false;
        }

        public IType checkArrowExpression(Context context, ArrowExpression expression)
        {
            context = context.newChildContext();
            this.method.registerParameters(context);
            expression.Check(context, this.method.getReturnType());
            return this;
        }
    }

}
