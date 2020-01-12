using System;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public class AlwaysValidator : BasePropertyValidator
    {
        public static AlwaysValidator Instance = new AlwaysValidator();

        private AlwaysValidator()
        {
        }

        public override IType GetIType(Context context)
        {
            return AnyType.Instance;
        }

      
        public override void Validate(Context context, JsxProperty prop)
        {
            // nothing to do
        }
    }
}
