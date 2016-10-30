using System;
using prompto.runtime;
using prompto.store;

namespace prompto.type
{

    public class EnumeratedCategoryType : CategoryType
    {

        public EnumeratedCategoryType(String name)
            : base(TypeFamily.ENUMERATED, name)
        {
        }

        
		public override IType checkMember(Context context, String name)
        {
            if ("value" == name)
                return this;
            else if ("symbol" == name)
                return TextType.Instance;
            else
                return base.checkMember(context, name);
        }

    }

}
