using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.parser;
using prompto.utils;

namespace prompto.grammar
{
    public interface IDialectElement
    {
        void ToDialect(CodeWriter writer);
    }
}
