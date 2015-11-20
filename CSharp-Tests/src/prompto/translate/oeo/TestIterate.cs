using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestIterate : BaseOParserTest
	{

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOEO("iterate/forEachIntegerList.poc");
		}

	}
}

