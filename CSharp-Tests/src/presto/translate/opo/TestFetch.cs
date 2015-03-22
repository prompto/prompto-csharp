using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestFetch : BaseOParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceOPO("fetch/fetchFromList.o");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceOPO("fetch/fetchFromSet.o");
		}

	}
}

