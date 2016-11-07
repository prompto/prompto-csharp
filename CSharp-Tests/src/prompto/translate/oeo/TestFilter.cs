using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestFilter : BaseOParserTest
	{

		[Test]
		public void testFilterFromList()
		{
			compareResourceOEO("filter/filterFromList.poc");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceOEO("filter/filterFromSet.poc");
		}

	}
}

