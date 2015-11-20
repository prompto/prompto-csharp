using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestIterate : BaseEParserTest
	{

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceESE("iterate/forEachIntegerList.pec");
		}

	}
}

