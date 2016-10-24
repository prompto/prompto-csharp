using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestSortList : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceESE("sortList/sortBooleans.pec");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceESE("sortList/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceESE("sortList/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceESE("sortList/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceESE("sortList/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceESE("sortList/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceESE("sortList/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceESE("sortList/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceESE("sortList/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceESE("sortList/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceESE("sortList/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceESE("sortList/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceESE("sortList/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceESE("sortList/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceESE("sortList/sortDescTimes.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceESE("sortList/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceESE("sortList/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceESE("sortList/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceESE("sortList/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceESE("sortList/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceESE("sortList/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceESE("sortList/sortTimes.pec");
		}

	}
}

