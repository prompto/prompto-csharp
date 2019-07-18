using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestFilter : BaseOParserTest
	{

		[Test]
		public void testFilterFromIterable()
		{
			compareResourceOMO("filter/filterFromIterable.poc");
		}

		[Test]
		public void testFilterFromList()
		{
			compareResourceOMO("filter/filterFromList.poc");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceOMO("filter/filterFromSet.poc");
		}

	}
}

