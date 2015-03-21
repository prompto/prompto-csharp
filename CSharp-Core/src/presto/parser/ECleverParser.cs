using presto.parser;
using presto.grammar;
using System;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;

namespace presto.parser {

public class ECleverParser : EParser, IParser {

	public ECleverParser(String input)
        : this(new AntlrInputStream(input))
    {
	}
	
	public ECleverParser(Stream input) 
		: this(new AntlrInputStream(input))
    {
	}
	
	public ECleverParser(String path, Stream input) 
 		: this(new AntlrInputStream(input))
   {
		Path = path;
	}

	public ECleverParser(ICharStream input) 
    	: this(new EIndentingLexer(input))
{
		}
	
	public ECleverParser(ITokenSource input) 
 		: this(new CommonTokenStream(input))
   {
	}

    public ECleverParser(ITokenStream input) 
 		: base(input)
   {
	}

    public string Path { set; get; }

	public DeclarationList Parse() {
        return parse_declaration_list();
    }
    
    public DeclarationList parse_declaration_list() {
		IParseTree tree = this.declaration_list();
		EPrestoBuilder builder = new EPrestoBuilder(this);
		ParseTreeWalker walker = new ParseTreeWalker();
		walker.Walk(builder, tree);
		return builder.GetNodeValue<DeclarationList>(tree);
	}
	
}

}