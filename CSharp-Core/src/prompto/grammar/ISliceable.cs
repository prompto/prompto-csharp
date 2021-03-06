using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.runtime;
using prompto.value;
using Boolean = prompto.value.BooleanValue;

namespace prompto.grammar
{
    public interface ISliceable : IContainer
    {
        ISliceable Slice(Context context, IntegerValue fi, IntegerValue li);
    }
}
