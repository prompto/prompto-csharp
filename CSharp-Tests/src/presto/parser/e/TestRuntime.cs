using presto.parser;
using NUnit.Framework;
using presto.utils;
using presto.java;
using presto.value;
using presto.runtime;
using presto.grammar;
using System;
using presto.csharp;
using presto.literal;
using presto.type;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace presto.parser.e {

    [TestFixture]
    public class TestNative : BaseEParserTest
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
        public void testSystemOutPrint()
        {
            ECleverParser parser = new ECleverParser("System.Console.Write(value);");
            ITokenStream stream = (ITokenStream)parser.InputStream;
            EIndentingLexer lexer = (EIndentingLexer)stream.TokenSource;
            lexer.AddLF = false;
  		    IParseTree tree = parser.csharp_statement();
		    EPrestoBuilder builder = new EPrestoBuilder(parser);
		    ParseTreeWalker walker = new ParseTreeWalker();
		    walker.Walk(builder, tree);
            CSharpStatement statement = builder.GetNodeValue<CSharpStatement>(tree);
            Context context = Context.newGlobalContext();
            IArgument arg = new CategoryArgument(TextType.Instance, "value");
            arg.register(context);
            context.setValue("value", new presto.value.Text("test")); // StringLiteral trims enclosing quotes
            Object result = statement.interpret(context, null);
            Assert.IsNull(result);
            Assert.AreEqual("test", Out.read());
        }

        [Test]
        public void testReturn()
        {
            runResource("native/return.pec");
            Assert.AreEqual(System.Environment.MachineName, Out.read());
        }

		[Test]
		public void testDateTimeTZName()
		{
			runResource("builtins/dateTimeTZName.pec");
			String tzName = TimeZoneInfo.Utc.StandardName;
			Assert.AreEqual("tzName=" + tzName, Out.read());
		}



    }

}
