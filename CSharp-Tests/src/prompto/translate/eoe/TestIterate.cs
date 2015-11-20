using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestIterate : BaseEParserTest
	{

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEOE("iterate/forEachIntegerList.pec");
		}

	}
}

