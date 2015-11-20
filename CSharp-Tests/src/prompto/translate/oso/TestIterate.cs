using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestIterate : BaseOParserTest
	{

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOSO("iterate/forEachIntegerList.poc");
		}

	}
}

