using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestFilter : BaseOParserTest
	{

		[Test]
		public void testFilterFromList()
		{
			compareResourceOSO("filter/filterFromList.poc");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceOSO("filter/filterFromSet.poc");
		}

	}
}

