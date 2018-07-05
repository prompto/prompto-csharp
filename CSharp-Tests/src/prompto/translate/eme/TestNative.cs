using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestNative : BaseEParserTest
	{

		[Test]
		public void testAnyId()
		{
			compareResourceEME("native/anyId.pec");
		}

		[Test]
		public void testAnyText()
		{
			compareResourceEME("native/anyText.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEME("native/attribute.pec");
		}

		[Test]
		public void testCategory()
		{
			compareResourceEME("native/category.pec");
		}

		[Test]
		public void testCategoryReturn()
		{
			compareResourceEME("native/categoryReturn.pec");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEME("native/method.pec");
		}

		[Test]
		public void testNow()
		{
			compareResourceEME("native/now.pec");
		}

		[Test]
		public void testPrinter()
		{
			compareResourceEME("native/printer.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEME("native/return.pec");
		}

	}
}

