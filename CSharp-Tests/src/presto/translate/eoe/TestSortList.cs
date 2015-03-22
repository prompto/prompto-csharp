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
			compareResourceEOE("sortList/sortBooleans.e");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEOE("sortList/sortDates.e");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEOE("sortList/sortDateTimes.e");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEOE("sortList/sortDecimals.e");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEOE("sortList/sortExpressions.e");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEOE("sortList/sortIntegers.e");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEOE("sortList/sortKeys.e");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEOE("sortList/sortMethods.e");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEOE("sortList/sortNames.e");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEOE("sortList/sortTexts.e");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEOE("sortList/sortTimes.e");
		}

	}
}

