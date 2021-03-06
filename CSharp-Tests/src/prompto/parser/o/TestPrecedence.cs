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

namespace prompto.parser.o {

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
            Context context = Context.newGlobalsContext();
            IntegerValue value = (IntegerValue)exp.interpret(context);
            Assert.AreEqual(-8L, value.LongValue);
        }

        [Test]
        public void test2Plus3Minuses()
        {
            IExpression exp = parse_expression("1+2-3+4-5-6");
            Context context = Context.newGlobalsContext();
            IntegerValue value = (IntegerValue)exp.interpret(context);
            Assert.AreEqual(-7L, value.LongValue);
        }

        [Test]
        public void test1Star1Plus()
        {
            IExpression exp = parse_expression("1*2+3");
            Context context = Context.newGlobalsContext();
            IntegerValue value = (IntegerValue)exp.interpret(context);
            Assert.AreEqual(5L, value.LongValue);
        }

        IExpression parse_expression(String code)
        {
            OCleverParser parser = new OCleverParser(code);
            IParseTree tree = parser.expression();
            OPromptoBuilder builder = new OPromptoBuilder(parser);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IExpression>(tree);
        }


    }

}
