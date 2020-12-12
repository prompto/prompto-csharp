using prompto.parser;
using prompto.declaration;
using System;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;
using prompto.type;

namespace prompto.parser
{

	public class MCleverParser : MParser, IParser
	{

		public MCleverParser (String input)
			: this (new AntlrInputStream (input))
		{
		}

		public MCleverParser (Stream input)
			: this (new AntlrInputStream (input))
		{
		}

		public MCleverParser (String path, Stream input)
			: this (new AntlrInputStream (input))
		{
			Path = path;
		}

		public MCleverParser (ICharStream input)
			: this (new MIndentingLexer (input))
		{
		}

		public MCleverParser (ITokenSource input)
			: this (new CommonTokenStream (input))
		{
		}

		public MCleverParser (ITokenStream input)
			: base (input)
		{
		}

		public MIndentingLexer getLexer()
		{
			return (MIndentingLexer)this.TokenStream.TokenSource;
		}

	
		public override int equalToken()
		{
			return MParser.EQ2;
		}

		public string Path { set; get; }

		public DeclarationList Parse ()
		{
			return parse_declaration_list ();
		}

		public DeclarationList parse_declaration_list()
		{
			return doParse<DeclarationList>(this.declaration_list, false);
		}

		public IType parse_standalone_type()
		{
			return doParse<IType>(this.category_or_any_type, false);
		}

		public T doParse<T>(Func<IParseTree> method, bool addLF)
		{
			getLexer().AddLF = addLF;
			IParseTree tree = method.Invoke();
			MPromptoBuilder builder = new MPromptoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<T>(tree);
		}
	}

}