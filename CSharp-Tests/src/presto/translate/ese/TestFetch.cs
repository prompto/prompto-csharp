using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestFetch : BaseEParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceESE("fetch/fetchFromList.pec");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceESE("fetch/fetchFromSet.pec");
		}

	}
}

