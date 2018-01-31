using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSortList : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceEOE("sortList/sortBooleans.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEOE("sortList/sortDateTimes.pec");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEOE("sortList/sortDates.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEOE("sortList/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceEOE("sortList/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceEOE("sortList/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceEOE("sortList/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceEOE("sortList/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceEOE("sortList/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceEOE("sortList/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceEOE("sortList/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceEOE("sortList/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceEOE("sortList/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceEOE("sortList/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceEOE("sortList/sortDescTimes.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEOE("sortList/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEOE("sortList/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEOE("sortList/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEOE("sortList/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEOE("sortList/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEOE("sortList/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEOE("sortList/sortTimes.pec");
		}

	}
}

