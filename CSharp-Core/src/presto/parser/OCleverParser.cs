using System;
using Antlr4.Runtime;
using System.IO;
using presto.grammar;
using Antlr4.Runtime.Tree;

namespace presto.parser
{

    public class OCleverParser : OParser, IParser
    {

        public OCleverParser(String input)
            : this(new AntlrInputStream(input))
        {
        }

        public OCleverParser(Stream input)
            : this(new AntlrInputStream(input))
        {
        }

        public OCleverParser(String path, Stream input)
            : this(new AntlrInputStream(input))
        {
            Path = path;
        }

        public OCleverParser(ICharStream input)
            : this(new ONamingLexer(input))
        {
        }

        public OCleverParser(ITokenSource input)
            : this(new CommonTokenStream(input))
        {
        }

        public OCleverParser(ITokenStream input)
            : base(input)
        {
        }

		override
		public int equalToken()
		{
			return OParser.EQ;
		}

		public string Path { set; get; }

        public DeclarationList Parse()
        {
            return parse_declaration_list();
        }

        public DeclarationList parse_declaration_list()
        {
            IParseTree tree = declaration_list();
            OPrestoBuilder builder = new OPrestoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<DeclarationList>(tree);
        }
    }
}