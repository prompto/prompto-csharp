using prompto.expression;

namespace prompto.grammar
{

    public class NativeSymbolList : SymbolList<NativeSymbol>
    {

		public NativeSymbolList()
		{
		}


		public NativeSymbolList(NativeSymbol symbol)
            : base(symbol)
        {
        }

        /* for unified grammar */
        public void add(NativeSymbol symbol)
        {
            this.Add(symbol);
        }
    }
}
