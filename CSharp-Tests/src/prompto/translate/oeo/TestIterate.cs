using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestIterate : BaseOParserTest
	{

		[Test]
		public void testForEachExpression()
		{
			compareResourceOEO("iterate/forEachExpression.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOEO("iterate/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOEO("iterate/forEachIntegerRange.poc");
		}

	}
}

