using System;
using Antlr4.Runtime;
using System.IO;

namespace presto.parser
{

    public class EParserFactory : IParserFactory
    {

        public ILexer newLexer(String data)
        {
            return new EIndentingLexer(new AntlrInputStream(data));
        }

        public IParser newParser(String data)
        {
            return new ECleverParser(data);
        }

        public ILexer newLexer(Stream data)
        {
            return new EIndentingLexer(new AntlrInputStream(data));
        }

        public IParser newParser(String path, Stream data)
        {
            return new ECleverParser(path, data);
        }

    }

}