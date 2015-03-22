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
			compareResourceEOE("native/category.e");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEOE("native/method.e");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEOE("native/return.e");
		}

	}
}

