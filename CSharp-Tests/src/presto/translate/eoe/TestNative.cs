using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestNative : BaseEParserTest
	{

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
		public void testPrint()
		{
			compareResourceEOE("native/print.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEOE("native/return.pec");
		}

	}
}

