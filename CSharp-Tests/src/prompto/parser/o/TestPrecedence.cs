using prompto.parser;
using NUnit.Framework;
using prompto.utils;
using prompto.grammar;
using prompto.runtime;
using System;
using prompto.value;
using prompto.expression;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
namespace prompto.o.runtime {

    [TestFixture]
    public class TestPrecedence : BaseOParserTest
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

        IExpression parse_expression(String code)
        {
            OCleverParser parser = new OCleverParser(code);
            IParseTree tree = parser.expression();
            OPrestoBuilder builder = new OPrestoBuilder(parser);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IExpression>(tree);
        }


    }

}
