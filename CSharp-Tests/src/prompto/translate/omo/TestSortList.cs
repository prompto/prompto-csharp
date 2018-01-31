using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOMO("sortList/sortBooleans.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOMO("sortList/sortDateTimes.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOMO("sortList/sortDates.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOMO("sortList/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceOMO("sortList/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceOMO("sortList/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceOMO("sortList/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceOMO("sortList/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceOMO("sortList/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceOMO("sortList/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceOMO("sortList/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceOMO("sortList/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceOMO("sortList/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceOMO("sortList/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceOMO("sortList/sortDescTimes.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOMO("sortList/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOMO("sortList/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOMO("sortList/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOMO("sortList/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOMO("sortList/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOMO("sortList/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOMO("sortList/sortTimes.poc");
		}

	}
}

