using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.runtime;
using prompto.value;
using Boolean = prompto.value.Boolean;

namespace prompto.grammar
{
    public interface ISliceable : IContainer
    {
        ISliceable Slice(Context context, Integer fi, Integer li);
    }
}
