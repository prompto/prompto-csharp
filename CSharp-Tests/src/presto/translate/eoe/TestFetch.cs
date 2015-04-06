using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestFetch : BaseEParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceEOE("fetch/fetchFromList.pec");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceEOE("fetch/fetchFromSet.pec");
		}

	}
}

