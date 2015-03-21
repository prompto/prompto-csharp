using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using presto.grammar;

namespace presto.declaration
{
	public interface IEnumeratedDeclaration<T> : IDeclaration where T : Symbol
    {
        SymbolList<T> getSymbols();
    }
}
