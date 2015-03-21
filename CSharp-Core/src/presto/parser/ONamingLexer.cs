using presto.parser;
using System.Collections.Generic;
using System;
using Antlr4.Runtime;

namespace presto.parser
{

    public class ONamingLexer : OLexer, ILexer
    {

        static Dictionary<int, String> tokenNames = ParserUtils.extractTokenNames(typeof(OLexer));

        public static String getTokenName(IToken t)
        {
            return tokenNames[t.Type];
        }

        public static String getTokenName(int t)
        {
            return tokenNames[t];
        }

        public ONamingLexer(ICharStream input)
            : base(input)
        {
        }

        public Dialect Dialect
        {
            get { return Dialect.O; }
        }
    }
}
