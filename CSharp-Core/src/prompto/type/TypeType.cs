using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.runtime;
using prompto.store;
using prompto.utils;

namespace prompto.type
{
    public class TypeType : BaseType
    {
        IType type;

        public TypeType(IType type)
            : base(TypeFamily.TYPE)
        {

          this.type = type;
        }

        public IType GetIType()
        {
            return type;
        }

        public override string ToString()
        {
            return "Type<" + type.ToString() + ">";
        }

        public override void ToDialect(CodeWriter writer)
        {
            writer.append("Type<");
            type.ToDialect(writer);
            writer.append(">");
        }

        public override void checkUnique(Context context)
        {
            // nothing to do
        }

        public override void checkExists(Context context)
        {
            // nothing to do
        }


        public override bool isMoreSpecificThan(Context context, IType other)
        {
            return false;
        }

    
        public override IType checkMember(Context context, String name)
        {
            return type.checkStaticMember(context, name);
        }

    
        public override ISet<IMethodDeclaration> getMemberMethods(Context context, String name) 
        {
		    return type.getStaticMemberMethods(context, name);
        }

        public override Type ToCSharpType(Context context)
        {
            return typeof(Type);
        }
    }
}
