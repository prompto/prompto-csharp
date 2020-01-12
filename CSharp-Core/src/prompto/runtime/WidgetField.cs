using System;
using prompto.type;

namespace prompto.runtime
{
    public class WidgetField : Variable
    {
        public WidgetField(String name, IType type)
            : base(name, type)
        {
        }
    }
}
