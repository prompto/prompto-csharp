using System;
using Antlr4.Runtime;
using System.IO;

namespace presto.parser {

    public class OParserFactory : IParserFactory
    {

        public ILexer newLexer(String data)
        {
            return new ONamingLexer(new AntlrInputStream(data));
        }

        public IParser newParser(String data)
        {
            return new OCleverParser(data);
        }

        public ILexer newLexer(Stream data)
        {
            try
            {
                return new ONamingLexer(new AntlrInputStream(data));
            }
            catch (IOException e)
            {
                throw new Exception(null, e);
            }
        }

        public IParser newParser(String path, Stream data)
        {
            try
            {
                return new OCleverParser(path, data);
            }
            catch (IOException e)
            {
                throw new Exception(null, e);
            }
        }

    }
}
