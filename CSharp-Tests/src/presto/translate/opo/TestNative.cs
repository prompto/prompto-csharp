using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestNative : BaseOParserTest
	{

		[Test]
		public void testCategory()
		{
			compareResourceOPO("native/category.o");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOPO("native/method.o");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOPO("native/return.o");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			compareResourceOPO("native/returnBooleanLiteral.o");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			compareResourceOPO("native/returnBooleanObject.o");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			compareResourceOPO("native/returnBooleanValue.o");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			compareResourceOPO("native/returnCharacterLiteral.o");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			compareResourceOPO("native/returnCharacterObject.o");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			compareResourceOPO("native/returnCharacterValue.o");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			compareResourceOPO("native/returnDecimalLiteral.o");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			compareResourceOPO("native/returnIntegerLiteral.o");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			compareResourceOPO("native/returnIntegerObject.o");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			compareResourceOPO("native/returnIntegerValue.o");
		}

		[Test]
		public void testReturnLongObject()
		{
			compareResourceOPO("native/returnLongObject.o");
		}

		[Test]
		public void testReturnLongValue()
		{
			compareResourceOPO("native/returnLongValue.o");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			compareResourceOPO("native/returnStringLiteral.o");
		}

	}
}

