using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestNative : BaseEParserTest
	{

		[Test]
		public void testAnyId()
		{
			compareResourceESE("native/anyId.pec");
		}

		[Test]
		public void testAnyText()
		{
			compareResourceESE("native/anyText.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceESE("native/attribute.pec");
		}

		[Test]
		public void testCategory()
		{
			compareResourceESE("native/category.pec");
		}

		[Test]
		public void testMethod()
		{
			compareResourceESE("native/method.pec");
		}

		[Test]
		public void testNow()
		{
			compareResourceESE("native/now.pec");
		}

		[Test]
		public void testPrinter()
		{
			compareResourceESE("native/printer.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceESE("native/return.pec");
		}

	}
}

