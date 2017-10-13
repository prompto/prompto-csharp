
using prompto.runtime;
using System;
using prompto.declaration;
using prompto.value;
using prompto.error;
using prompto.store;

namespace prompto.type
{

    public class EnumeratedNativeType : BaseType
    {

        NativeType derivedFrom;
		String typeName;

        public EnumeratedNativeType(String typeName, NativeType derivedFrom)
			: base(TypeFamily.ENUMERATED)
         {
           this.derivedFrom = derivedFrom;
			this.typeName = typeName;
        }

		public override string GetTypeName()
		{
			return typeName;
		}

        
		public override IType checkMember(Context context, String name)
        {
            if ("symbols" == name)
			    return new ListType(this);
		    else if ("value" == name)
                return this.derivedFrom;
            else if ("name" == name)
                return TextType.Instance;
            else
                return base.checkMember(context, name);
        }

        
        public override IValue getMemberValue(Context context, String name)
        {
            IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(this.typeName);
            if (!(decl is EnumeratedNativeDeclaration))
                throw new SyntaxError(name + " is not an enumerated type!");
            if ("symbols" == name)
                return ((EnumeratedNativeDeclaration)decl).getSymbols();
            else
                throw new SyntaxError("Unknown member:" + name);
        }
	    

        public NativeType getDerivedFrom()
        {
            return derivedFrom;
        }

        
        public override System.Type ToCSharpType()
        {
            // TODO Auto-generated method stub
            return null;
        }

        
        public override void checkUnique(Context context)
        {
            // TODO Auto-generated method stub

        }

        
        public override void checkExists(Context context)
        {
            // TODO Auto-generated method stub

        }

        
        public override bool isAssignableFrom(Context context, IType other)
        {
 			return this.GetTypeName().Equals(other.GetTypeName());
       }

        
        public override bool isMoreSpecificThan(Context context, IType other)
        {
            // TODO Auto-generated method stub
            return false;
        }

    }

}