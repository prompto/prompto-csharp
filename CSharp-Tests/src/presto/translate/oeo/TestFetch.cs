using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestFetch : BaseOParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceOEO("fetch/fetchFromList.o");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceOEO("fetch/fetchFromSet.o");
		}

	}
}

