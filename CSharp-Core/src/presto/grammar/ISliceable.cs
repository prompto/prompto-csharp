using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using presto.runtime;
using presto.value;
using Boolean = presto.value.Boolean;

namespace presto.grammar
{
    public interface ISliceable : IContainer
    {
        ISliceable Slice(Context context, Integer fi, Integer li);
    }
}
