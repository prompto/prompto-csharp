using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
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
		public void testMethod()
		{
			compareResourceEOE("native/method.pec");
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

