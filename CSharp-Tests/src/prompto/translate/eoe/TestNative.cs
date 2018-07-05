using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestNative : BaseEParserTest
	{

		[Test]
		public void testAnyId()
		{
			compareResourceEOE("native/anyId.pec");
		}

		[Test]
		public void testAnyText()
		{
			compareResourceEOE("native/anyText.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEOE("native/attribute.pec");
		}

		[Test]
		public void testCategory()
		{
			compareResourceEOE("native/category.pec");
		}

		[Test]
		public void testCategoryReturn()
		{
			compareResourceEOE("native/categoryReturn.pec");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEOE("native/method.pec");
		}

		[Test]
		public void testNow()
		{
			compareResourceEOE("native/now.pec");
		}

		[Test]
		public void testPrinter()
		{
			compareResourceEOE("native/printer.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEOE("native/return.pec");
		}

	}
}

