// generated: 2015-07-05T23:01:01.258
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

