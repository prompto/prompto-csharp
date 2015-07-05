using prompto.runtime;
using prompto.error;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using prompto.declaration;

namespace prompto.type
{

    public class MethodType : BaseType
    {

        Context context;

        public MethodType(Context context, String name)
            : base(name)
        {
            this.context = context;
        }

		public Context GetContext()
		{
			return context;
		}

        override
        public Type ToCSharpType()
        {
            return typeof(MethodInfo);
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is MethodType))
                return false;
            MethodType other = (MethodType)obj;
			return this.GetName().Equals(other.GetName());
        }

        override
        public void checkUnique(Context context)
        {
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>(name);
            if (actual != null)
                throw new SyntaxError("Duplicate name: \"" + name + "\"");
        }

        IMethodDeclaration getDeclaration(Context context) {
            MethodDeclarationMap map = this.context.getRegisteredDeclaration<MethodDeclarationMap>(name);
		    if(map==null)
			    throw new SyntaxError("Unknown method: \"" + name + "\"");
            IEnumerator<IMethodDeclaration> emd = map.Values.GetEnumerator();
            emd.MoveNext();
            return emd.Current;
	    }

        override
        public void checkExists(Context context)
        {
            getDeclaration(context);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            if (!(other is MethodType))
                return false;
            MethodType otherType = (MethodType)other;
            try
            {
                IMethodDeclaration thisMethod = getDeclaration(context);
                IMethodDeclaration otherMethod = otherType.getDeclaration(context);
                return thisMethod.getProto(context).Equals(otherMethod.getProto(otherType.context)); // TODO: refine
            }
            catch (SyntaxError)
            {
                return false;
            }
        }

        override
        public bool isMoreSpecificThan(Context context, IType other)
        {
            // TODO Auto-generated method stub
            return false;
        }

    }

}
