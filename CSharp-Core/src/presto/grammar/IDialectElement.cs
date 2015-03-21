using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using presto.parser;
using presto.utils;

namespace presto.grammar
{
    public interface IDialectElement
    {
        void ToDialect(CodeWriter writer);
    }
}
