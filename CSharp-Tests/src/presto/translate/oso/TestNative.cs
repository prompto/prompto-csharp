using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestNative : BaseOParserTest
	{

		[Test]
		public void testCategory()
		{
			compareResourceOSO("native/category.poc");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOSO("native/method.poc");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOSO("native/return.poc");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			compareResourceOSO("native/returnBooleanLiteral.poc");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			compareResourceOSO("native/returnBooleanObject.poc");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			compareResourceOSO("native/returnBooleanValue.poc");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			compareResourceOSO("native/returnCharacterLiteral.poc");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			compareResourceOSO("native/returnCharacterObject.poc");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			compareResourceOSO("native/returnCharacterValue.poc");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			compareResourceOSO("native/returnDecimalLiteral.poc");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			compareResourceOSO("native/returnIntegerLiteral.poc");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			compareResourceOSO("native/returnIntegerObject.poc");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			compareResourceOSO("native/returnIntegerValue.poc");
		}

		[Test]
		public void testReturnLongObject()
		{
			compareResourceOSO("native/returnLongObject.poc");
		}

		[Test]
		public void testReturnLongValue()
		{
			compareResourceOSO("native/returnLongValue.poc");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			compareResourceOSO("native/returnStringLiteral.poc");
		}

	}
}

