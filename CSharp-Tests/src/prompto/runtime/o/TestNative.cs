using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("native/category.poc");
		}

		[Test]
		public void testCategoryReturn()
		{
			CheckOutput("native/categoryReturn.poc");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("native/method.poc");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			CheckOutput("native/returnBooleanLiteral.poc");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			CheckOutput("native/returnBooleanObject.poc");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			CheckOutput("native/returnBooleanValue.poc");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			CheckOutput("native/returnCharacterLiteral.poc");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			CheckOutput("native/returnCharacterObject.poc");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			CheckOutput("native/returnCharacterValue.poc");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			CheckOutput("native/returnDecimalLiteral.poc");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			CheckOutput("native/returnIntegerLiteral.poc");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			CheckOutput("native/returnIntegerObject.poc");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			CheckOutput("native/returnIntegerValue.poc");
		}

		[Test]
		public void testReturnLongLiteral()
		{
			CheckOutput("native/returnLongLiteral.poc");
		}

		[Test]
		public void testReturnLongObject()
		{
			CheckOutput("native/returnLongObject.poc");
		}

		[Test]
		public void testReturnLongValue()
		{
			CheckOutput("native/returnLongValue.poc");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			CheckOutput("native/returnStringLiteral.poc");
		}

	}
}

