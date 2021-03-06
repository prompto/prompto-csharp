using prompto.expression;

namespace prompto.grammar
{

    public class CategorySymbolList : SymbolList<CategorySymbol>
    {

		public CategorySymbolList()
		{
		}

		public CategorySymbolList(CategorySymbol symbol)
            : base(symbol)
        {
        }

        /* for unified grammar */
        public void add(CategorySymbol symbol)
        {
            this.Add(symbol);
        }
    }
}
