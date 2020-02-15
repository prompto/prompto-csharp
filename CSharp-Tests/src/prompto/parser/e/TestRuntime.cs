using prompto.parser;
using NUnit.Framework;
using prompto.utils;
using prompto.java;
using prompto.value;
using prompto.runtime;
using prompto.grammar;
using System;
using prompto.csharp;
using prompto.literal;
using prompto.type;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using prompto.param;

namespace prompto.parser.e {

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
		    EPromptoBuilder builder = new EPromptoBuilder(parser);
		    ParseTreeWalker walker = new ParseTreeWalker();
		    walker.Walk(builder, tree);
            CSharpStatement statement = builder.GetNodeValue<CSharpStatement>(tree);
            Context context = Context.newGlobalContext();
            IParameter arg = new CategoryParameter(TextType.Instance, "value");
            arg.register(context);
            context.setValue("value", new prompto.value.TextValue("test")); // StringLiteral trims enclosing quotes
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
