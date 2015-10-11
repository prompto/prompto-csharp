using prompto.parser;
using prompto.declaration;
using System;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;

namespace prompto.parser
{

	public class SCleverParser : SParser, IParser
	{

		public SCleverParser (String input)
			: this (new AntlrInputStream (input))
		{
		}

		public SCleverParser (Stream input)
			: this (new AntlrInputStream (input))
		{
		}

		public SCleverParser (String path, Stream input)
			: this (new AntlrInputStream (input))
		{
			Path = path;
		}

		public SCleverParser (ICharStream input)
			: this (new SIndentingLexer (input))
		{
		}

		public SCleverParser (ITokenSource input)
			: this (new CommonTokenStream (input))
		{
		}

		public SCleverParser (ITokenStream input)
			: base (input)
		{
		}

		override
		public int equalToken()
		{
			return SParser.EQ2;
		}

		public string Path { set; get; }

		public DeclarationList Parse ()
		{
			return parse_declaration_list ();
		}

		public DeclarationList parse_declaration_list ()
		{
			IParseTree tree = this.declaration_list ();
			SPrestoBuilder builder = new SPrestoBuilder (this);
			ParseTreeWalker walker = new ParseTreeWalker ();
			walker.Walk (builder, tree);
			return builder.GetNodeValue<DeclarationList> (tree);
		}
	
	}

}