// generated: 2015-07-05T23:01:01.359
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestNative : BaseOParserTest
	{

		[Test]
		public void testCategory()
		{
			compareResourceOEO("native/category.poc");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOEO("native/method.poc");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOEO("native/return.poc");
		}

		[Test]
		public void testReturnBooleanLiteral()
		{
			compareResourceOEO("native/returnBooleanLiteral.poc");
		}

		[Test]
		public void testReturnBooleanObject()
		{
			compareResourceOEO("native/returnBooleanObject.poc");
		}

		[Test]
		public void testReturnBooleanValue()
		{
			compareResourceOEO("native/returnBooleanValue.poc");
		}

		[Test]
		public void testReturnCharacterLiteral()
		{
			compareResourceOEO("native/returnCharacterLiteral.poc");
		}

		[Test]
		public void testReturnCharacterObject()
		{
			compareResourceOEO("native/returnCharacterObject.poc");
		}

		[Test]
		public void testReturnCharacterValue()
		{
			compareResourceOEO("native/returnCharacterValue.poc");
		}

		[Test]
		public void testReturnDecimalLiteral()
		{
			compareResourceOEO("native/returnDecimalLiteral.poc");
		}

		[Test]
		public void testReturnIntegerLiteral()
		{
			compareResourceOEO("native/returnIntegerLiteral.poc");
		}

		[Test]
		public void testReturnIntegerObject()
		{
			compareResourceOEO("native/returnIntegerObject.poc");
		}

		[Test]
		public void testReturnIntegerValue()
		{
			compareResourceOEO("native/returnIntegerValue.poc");
		}

		[Test]
		public void testReturnLongObject()
		{
			compareResourceOEO("native/returnLongObject.poc");
		}

		[Test]
		public void testReturnLongValue()
		{
			compareResourceOEO("native/returnLongValue.poc");
		}

		[Test]
		public void testReturnStringLiteral()
		{
			compareResourceOEO("native/returnStringLiteral.poc");
		}

	}
}

