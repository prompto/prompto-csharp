using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.grammar;

namespace prompto.declaration
{
	public interface IEnumeratedDeclaration<T> : IDeclaration where T : Symbol
    {
        SymbolList<T> getSymbols();
    }
}
