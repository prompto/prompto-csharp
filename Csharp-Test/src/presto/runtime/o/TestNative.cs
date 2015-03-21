using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestNative : BaseOParserTest
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
		public void testCategory()
		{
			CheckOutput("native/category.o");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("native/method.o");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			CheckOutput("native/returnBooleanLiteral.o");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			CheckOutput("native/returnBooleanObject.o");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			CheckOutput("native/returnBooleanValue.o");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			CheckOutput("native/returnCharacterLiteral.o");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			CheckOutput("native/returnCharacterObject.o");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			CheckOutput("native/returnCharacterValue.o");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			CheckOutput("native/returnDecimalLiteral.o");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			CheckOutput("native/returnIntegerLiteral.o");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			CheckOutput("native/returnIntegerObject.o");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			CheckOutput("native/returnIntegerValue.o");
		}

		[Test]
		public void testReturnLongObject()
		{
			CheckOutput("native/returnLongObject.o");
		}

		[Test]
		public void testReturnLongValue()
		{
			CheckOutput("native/returnLongValue.o");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			CheckOutput("native/returnStringLiteral.o");
		}

	}
}

