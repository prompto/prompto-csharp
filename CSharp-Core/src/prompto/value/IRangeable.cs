using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.grammar;

namespace prompto.value
{
    public interface IRangeable
    {
        IRange NewRange();
    }
}
