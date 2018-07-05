using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestNative : BaseOParserTest
	{

		[Test]
		public void testCategory()
		{
			compareResourceOMO("native/category.poc");
		}

		[Test]
		public void testCategoryReturn()
		{
			compareResourceOMO("native/categoryReturn.poc");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOMO("native/method.poc");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOMO("native/return.poc");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			compareResourceOMO("native/returnBooleanLiteral.poc");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			compareResourceOMO("native/returnBooleanObject.poc");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			compareResourceOMO("native/returnBooleanValue.poc");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			compareResourceOMO("native/returnCharacterLiteral.poc");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			compareResourceOMO("native/returnCharacterObject.poc");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			compareResourceOMO("native/returnCharacterValue.poc");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			compareResourceOMO("native/returnDecimalLiteral.poc");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			compareResourceOMO("native/returnIntegerLiteral.poc");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			compareResourceOMO("native/returnIntegerObject.poc");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			compareResourceOMO("native/returnIntegerValue.poc");
		}

		[Test]
		public void testReturnLongLiteral()
		{
			compareResourceOMO("native/returnLongLiteral.poc");
		}

		[Test]
		public void testReturnLongObject()
		{
			compareResourceOMO("native/returnLongObject.poc");
		}

		[Test]
		public void testReturnLongValue()
		{
			compareResourceOMO("native/returnLongValue.poc");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			compareResourceOMO("native/returnStringLiteral.poc");
		}

	}
}

