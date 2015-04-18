using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestNative : BaseEParserTest
	{

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
		public void testPrint()
		{
			compareResourceESE("native/print.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceESE("native/return.pec");
		}

	}
}
