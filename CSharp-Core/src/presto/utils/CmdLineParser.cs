

using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;

namespace presto.utils {

public class CmdLineParser {

    public static Dictionary<String, String> parse(String input)
    {
		if(input==null)
			input = "";
		try {
			ICharStream stream = new AntlrInputStream(input);
			ArgsLexer lexer = new ArgsLexer(stream);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			ArgsParser parser = new ArgsParser(tokens);
            IParseTree tree = parser.parse();
            CmdLineBuilder builder = new CmdLineBuilder();
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.getCmdLineArgs();
 		} catch (RecognitionException e) {
            Console.WriteLine(e.ToString());
			throw e;
		}
	}
	
	
}
    	class CmdLineBuilder : ArgsParserBaseListener {
		
		Dictionary<String,String> args = new Dictionary<String,String>();
		ParseTreeProperty<String> values = new ParseTreeProperty<String>();
		
		public Dictionary<String, String> getCmdLineArgs() {
			return args;
		}

		public void exitEntry(ArgsParser.EntryContext ctx) {
			String key = values.Get(ctx.k);
			String value = values.Get(ctx.v);
			args[key] = value;
		}

		public void exitKey(ArgsParser.KeyContext ctx) {
			values.Put(ctx, ctx.GetText());
		}

		public void exitSTRING(ArgsParser.STRINGContext ctx) {
			String s = ctx.GetText();
			values.Put(ctx, s.Substring(1,s.Length-2));
		}

		public void exitELEMENT(ArgsParser.ELEMENTContext ctx) {
			values.Put(ctx, ctx.GetText());
		}
		 
	}
}