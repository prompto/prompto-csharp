using prompto.parser;
using prompto.declaration;
using System;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;
using prompto.type;

namespace prompto.parser
{

	public class ECleverParser : EParser, IParser
	{

		public ECleverParser (String input)
			: this (new AntlrInputStream (input))
		{
		}

		public ECleverParser (Stream input)
			: this (new AntlrInputStream (input))
		{
		}

		public ECleverParser (String path, Stream input)
			: this (new AntlrInputStream (input))
		{
			Path = path;
		}

		public ECleverParser (ICharStream input)
			: this (new EIndentingLexer (input))
		{
		}

		public ECleverParser (ITokenSource input)
			: this (new CommonTokenStream (input))
		{
		}

		public ECleverParser (ITokenStream input)
			: base (input)
		{
		}


		public EIndentingLexer getLexer ()
		{
			return (EIndentingLexer)this.TokenStream.TokenSource;
		}

		public string Path { set; get; }

		public DeclarationList Parse ()
		{
			return parse_declaration_list ();
		}

		public DeclarationList parse_declaration_list ()
		{
			IParseTree tree = this.declaration_list ();
			EPromptoBuilder builder = new EPromptoBuilder (this);
			ParseTreeWalker walker = new ParseTreeWalker ();
			walker.Walk (builder, tree);
			return builder.GetNodeValue<DeclarationList> (tree);
		}

		public IType parse_standalone_type ()
		{
			getLexer ().AddLF = false;
			IParseTree tree = this.category_or_any_type ();
			EPromptoBuilder builder = new EPromptoBuilder (this);
			ParseTreeWalker walker = new ParseTreeWalker ();
			walker.Walk (builder, tree);
			return builder.GetNodeValue<IType> (tree);
		}
	}

}