using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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
		public void testSortDescBooleans()
		{
			compareResourceOEO("sortSet/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceOEO("sortSet/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceOEO("sortSet/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceOEO("sortSet/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceOEO("sortSet/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceOEO("sortSet/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceOEO("sortSet/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceOEO("sortSet/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceOEO("sortSet/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceOEO("sortSet/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceOEO("sortSet/sortDescTimes.poc");
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

