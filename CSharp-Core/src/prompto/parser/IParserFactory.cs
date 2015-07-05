using System;
using System.IO;
namespace prompto.parser
{

    public interface IParserFactory
    {

        ILexer newLexer(String data);
        ILexer newLexer(Stream data);
        IParser newParser(String data);
        IParser newParser(String path, Stream data);
    }
}