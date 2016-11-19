using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestIterate : BaseOParserTest
	{

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOMO("iterate/forEachIntegerList.poc");
		}

	}
}

