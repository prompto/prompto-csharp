using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestNative : BaseEParserTest
	{

		[Test]
		public void testCategory()
		{
			compareResourceEPE("native/category.e");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEPE("native/method.e");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEPE("native/return.e");
		}

	}
}

