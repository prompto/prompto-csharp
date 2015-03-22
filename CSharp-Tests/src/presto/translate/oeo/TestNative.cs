using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestNative : BaseOParserTest
	{

		[Test]
		public void testCategory()
		{
			compareResourceOEO("native/category.o");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOEO("native/method.o");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOEO("native/return.o");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			compareResourceOEO("native/returnBooleanLiteral.o");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			compareResourceOEO("native/returnBooleanObject.o");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			compareResourceOEO("native/returnBooleanValue.o");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			compareResourceOEO("native/returnCharacterLiteral.o");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			compareResourceOEO("native/returnCharacterObject.o");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			compareResourceOEO("native/returnCharacterValue.o");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			compareResourceOEO("native/returnDecimalLiteral.o");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			compareResourceOEO("native/returnIntegerLiteral.o");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			compareResourceOEO("native/returnIntegerObject.o");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			compareResourceOEO("native/returnIntegerValue.o");
		}

		[Test]
		public void testReturnLongObject()
		{
			compareResourceOEO("native/returnLongObject.o");
		}

		[Test]
		public void testReturnLongValue()
		{
			compareResourceOEO("native/returnLongValue.o");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			compareResourceOEO("native/returnStringLiteral.o");
		}

	}
}

