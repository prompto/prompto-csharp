using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOEO("sortList/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOEO("sortList/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOEO("sortList/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOEO("sortList/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceOEO("sortList/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceOEO("sortList/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceOEO("sortList/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceOEO("sortList/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceOEO("sortList/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceOEO("sortList/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceOEO("sortList/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceOEO("sortList/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceOEO("sortList/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceOEO("sortList/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceOEO("sortList/sortDescTimes.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOEO("sortList/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOEO("sortList/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOEO("sortList/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOEO("sortList/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOEO("sortList/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOEO("sortList/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOEO("sortList/sortTimes.poc");
		}

	}
}

