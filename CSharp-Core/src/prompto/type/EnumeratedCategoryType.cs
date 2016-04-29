using System;
using prompto.runtime;

namespace prompto.type
{

    public class EnumeratedCategoryType : CategoryType
    {

        public EnumeratedCategoryType(String name)
            : base(name)
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
