using presto.parser;
using NUnit.Framework;
using presto.utils;
using presto.grammar;
using presto.runtime;
using System;
using presto.value;
using presto.expression;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace presto.parser.e {

    [TestFixture]
    public class TestPrecedence : BaseEParserTest
    {

        [SetUp]
        public void before()
        {
            Out.init();
        }

        [TearDown]
        public void after()
        {
            Out.restore();
        }

        [Test]
        public void test3Minuses()
        {
            IExpression exp = parse_expression("1-2-3-4");
            Context context = Context.newGlobalContext();
            Integer value = (Integer)exp.interpret(context);
            Assert.AreEqual(-8L, value.IntegerValue);
        }

        [Test]
        public void test2Plus3Minuses()
        {
            IExpression exp = parse_expression("1+2-3+4-5-6");
            Context context = Context.newGlobalContext();
            Integer value = (Integer)exp.interpret(context);
            Assert.AreEqual(-7L, value.IntegerValue);
        }

        [Test]
        public void test1Star1Plus()
        {
            IExpression exp = parse_expression("1*2+3");
            Context context = Context.newGlobalContext();
            Integer value = (Integer)exp.interpret(context);
            Assert.AreEqual(5L, value.IntegerValue);
        }

        IExpression parse_expression(String code) {
		    ECleverParser parser = new ECleverParser(code);
            ITokenStream stream = (ITokenStream)parser.InputStream;
            EIndentingLexer lexer = (EIndentingLexer)stream.TokenSource;
 		    lexer.AddLF = false;
		    IParseTree tree = parser.expression();
		    EPrestoBuilder builder = new EPrestoBuilder(parser);
		    ParseTreeWalker walker = new ParseTreeWalker();
		    walker.Walk(builder, tree);
		    return builder.GetNodeValue<IExpression>(tree);
        }


    }

}
