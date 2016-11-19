using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestFilter : BaseEParserTest
	{

		[Test]
		public void testFilterFromList()
		{
			compareResourceEME("filter/filterFromList.pec");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceEME("filter/filterFromSet.pec");
		}

	}
}

