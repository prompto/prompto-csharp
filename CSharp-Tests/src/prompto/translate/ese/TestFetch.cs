// generated: 2015-07-05T23:01:01.259
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

