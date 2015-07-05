using System.Collections.Generic;
using System;
using System.IO;
using NUnit.Framework;
using Antlr4.Runtime;
namespace prompto.parser {

    public class BasePLexerTest
    {

        public List<IToken> parseTokens(Lexer lexer)
        {
            List<IToken> result = new List<IToken>();
            IToken t = lexer.NextToken();
            while (t.Type != SLexer.Eof)
            {
                if (t.Channel != Lexer.Hidden)
                    result.Add(t);
                t = lexer.NextToken();
            }
            return result;
        }

        public int[] parseTokenTypes(Lexer lexer)
        {
            List<IToken> tokens = parseTokens(lexer);
            int[] result = new int[tokens.Count];
            int i = 0;
            foreach (IToken t in tokens)
                result[i++] = t.Type;
            return result;
        }

        public String parseTokenNames(Lexer lexer)
        {
            List<IToken> tokens = parseTokens(lexer);
            String s = "";
            foreach (IToken t in tokens)
                s += ONamingLexer.getTokenName(t) + " ";
            return s.Substring(0, s.Length - 1);
        }

        public Lexer newTokenStreamFromString(String input)
        {
            ICharStream stream = new AntlrInputStream(input);
            return new OLexer(stream);
        }

        public Lexer newTokenStreamFromResource(String resourceName)
        {
            Stream input = OpenResource(resourceName);
            try
            {
                ICharStream stream = new AntlrInputStream(input);
                return new OLexer(stream);
            }
			finally
			{
				input.Close();
			}
       }

        private Stream OpenResource(String resourceName)
        {
            String resourceDir = Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))));
            String fullPath = resourceDir + "\\Test\\" + resourceName;
			Assert.IsTrue(File.Exists(fullPath), "resource not found:" + fullPath);
            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }
        
        public String parseTokenNamesFromString(String input)
        {
            Lexer lexer = newTokenStreamFromString(input);
            return parseTokenNames(lexer);
        }

        public String parseTokenNamesFromResource(String input)
        {
            Lexer lexer = newTokenStreamFromResource(input);
            return parseTokenNames(lexer);
        }

        protected String tokenNamesAsString(int[] tokenTypes)
        {
            String s = "";
            foreach (int t in tokenTypes)
                s += ONamingLexer.getTokenName(t) + " ";
            return s.Substring(0, s.Length - 1);
        }

    }

}
