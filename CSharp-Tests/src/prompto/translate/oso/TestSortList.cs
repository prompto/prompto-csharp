using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOSO("sortList/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOSO("sortList/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOSO("sortList/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOSO("sortList/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceOSO("sortList/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceOSO("sortList/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceOSO("sortList/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceOSO("sortList/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceOSO("sortList/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceOSO("sortList/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceOSO("sortList/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceOSO("sortList/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceOSO("sortList/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceOSO("sortList/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceOSO("sortList/sortDescTimes.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOSO("sortList/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOSO("sortList/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOSO("sortList/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOSO("sortList/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOSO("sortList/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOSO("sortList/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOSO("sortList/sortTimes.poc");
		}

	}
}

