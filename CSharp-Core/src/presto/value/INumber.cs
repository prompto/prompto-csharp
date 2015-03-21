using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace presto.value
{
    public interface INumber : IValue
    {
        long IntegerValue { get; }
        double DecimalValue { get; }
    }
}
