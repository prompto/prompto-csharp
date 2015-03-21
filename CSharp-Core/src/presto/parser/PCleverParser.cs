using presto.parser;
using presto.grammar;
using System;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;

namespace presto.parser
{

	public class PCleverParser : PParser, IParser
	{

		public PCleverParser (String input)
			: this (new AntlrInputStream (input))
		{
		}

		public PCleverParser (Stream input)
			: this (new AntlrInputStream (input))
		{
		}

		public PCleverParser (String path, Stream input)
			: this (new AntlrInputStream (input))
		{
			Path = path;
		}

		public PCleverParser (ICharStream input)
			: this (new PIndentingLexer (input))
		{
		}

		public PCleverParser (ITokenSource input)
			: this (new CommonTokenStream (input))
		{
		}

		public PCleverParser (ITokenStream input)
			: base (input)
		{
		}

		override
		public int equalToken()
		{
			return PParser.EQ2;
		}

		public string Path { set; get; }

		public DeclarationList Parse ()
		{
			return parse_declaration_list ();
		}

		public DeclarationList parse_declaration_list ()
		{
			IParseTree tree = this.declaration_list ();
			PPrestoBuilder builder = new PPrestoBuilder (this);
			ParseTreeWalker walker = new ParseTreeWalker ();
			walker.Walk (builder, tree);
			return builder.GetNodeValue<DeclarationList> (tree);
		}
	
	}

}