namespace presto.parser
{

    public enum Dialect
    {
        E, // (new EParserFactory()),
        O, // (new OParserFactory());
		P // (new PParserFactory());
    }
    /*	IParserFactory parserFactory;
	
        Dialect(IParserFactory factory) {
            this.parserFactory = factory;
        }
	
        public IParserFactory getParserFactory() {
            return parserFactory;
        }

    }*/

}