using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
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
		public void testSortDates()
		{
			compareResourceEOE("sortList/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEOE("sortList/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEOE("sortList/sortDecimals.pec");
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

