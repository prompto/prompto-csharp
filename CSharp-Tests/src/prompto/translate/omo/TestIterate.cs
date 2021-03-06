using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestIterate : BaseOParserTest
	{

		[Test]
		public void testForEachExpression()
		{
			compareResourceOMO("iterate/forEachExpression.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOMO("iterate/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOMO("iterate/forEachIntegerRange.poc");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceOMO("iterate/forEachIntegerSet.poc");
		}

	}
}

