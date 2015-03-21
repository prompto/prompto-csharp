using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestFetch : BaseEParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceEPE("fetch/fetchFromList.e");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceEPE("fetch/fetchFromSet.e");
		}

	}
}

