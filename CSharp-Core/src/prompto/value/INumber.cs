using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prompto.value
{
    public interface INumber : IValue
    {
        long LongValue { get; }
        double DoubleValue { get; }
    }
}
