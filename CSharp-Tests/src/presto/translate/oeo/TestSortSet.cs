using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSortSet : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOEO("sortSet/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOEO("sortSet/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOEO("sortSet/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOEO("sortSet/sortDecimals.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOEO("sortSet/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOEO("sortSet/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOEO("sortSet/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOEO("sortSet/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOEO("sortSet/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOEO("sortSet/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOEO("sortSet/sortTimes.poc");
		}

	}
}

