using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

