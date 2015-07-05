// generated: 2015-07-05T23:01:01.262
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestFetch : BaseOParserTest
	{

		[Test]
		public void testFetchFromList()
		{
			compareResourceOSO("fetch/fetchFromList.poc");
		}

		[Test]
		public void testFetchFromSet()
		{
			compareResourceOSO("fetch/fetchFromSet.poc");
		}

	}
}

