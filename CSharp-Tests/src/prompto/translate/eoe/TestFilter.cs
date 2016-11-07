using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestFilter : BaseEParserTest
	{

		[Test]
		public void testFilterFromList()
		{
			compareResourceEOE("filter/filterFromList.pec");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceEOE("filter/filterFromSet.pec");
		}

	}
}

