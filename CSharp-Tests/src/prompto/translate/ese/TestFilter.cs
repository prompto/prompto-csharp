using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestFilter : BaseEParserTest
	{

		[Test]
		public void testFilterFromList()
		{
			compareResourceESE("filter/filterFromList.pec");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceESE("filter/filterFromSet.pec");
		}

	}
}

