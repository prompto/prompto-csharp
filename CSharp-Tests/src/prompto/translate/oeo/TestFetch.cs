using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestFetch : BaseOParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceOEO("fetch/fetchFromList.poc");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceOEO("fetch/fetchFromSet.poc");
		}

	}
}

