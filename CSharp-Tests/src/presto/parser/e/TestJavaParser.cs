using NUnit.Framework;
using System;
using presto.java;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
namespace presto.parser {

    [TestFixture]
    public class TestJavaParser
    {

        [Test]
        public void testReturn()
        {
            String statement = "return value;";
            JavaStatement stmt = parse_java_statement(statement);
            Assert.IsNotNull(stmt);
            Assert.AreEqual(statement, stmt.ToString());
        }

        [Test]
        public void testExpression()
        {
            String statement = "System.str;";
            JavaStatement stmt = parse_java_statement(statement);
            Assert.IsNotNull(stmt);
            Assert.AreEqual(statement, stmt.ToString());
        }

        [Test]
        public void testArray()
        {
            String statement = "value[15];";
            JavaStatement stmt = parse_java_statement(statement);
            Assert.IsNotNull(stmt);
            Assert.AreEqual(statement, stmt.ToString());
        }

        [Test]
        public void testFunction()
        {
            String statement = "Console.Write(value);";
            JavaStatement stmt = parse_java_statement(statement);
            Assert.IsNotNull(stmt);
            Assert.AreEqual(statement, stmt.ToString());
        }

        JavaStatement parse_java_statement(String code) {
		ECleverParser parser = new ECleverParser(code);
	         ITokenStream stream = (ITokenStream)parser.InputStream;
            EIndentingLexer lexer = (EIndentingLexer)stream.TokenSource;
 		    lexer.AddLF = false;
		IParseTree tree = parser.java_statement();
		EPrestoBuilder builder = new EPrestoBuilder(parser);
		ParseTreeWalker walker = new ParseTreeWalker();
		walker.Walk(builder, tree);
		return builder.GetNodeValue<JavaStatement>(tree);
	}


    }

}