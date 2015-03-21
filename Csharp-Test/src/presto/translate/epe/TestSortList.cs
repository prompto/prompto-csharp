using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSortList : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceEPE("sortList/sortBooleans.e");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEPE("sortList/sortDates.e");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEPE("sortList/sortDateTimes.e");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEPE("sortList/sortDecimals.e");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEPE("sortList/sortExpressions.e");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEPE("sortList/sortIntegers.e");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEPE("sortList/sortKeys.e");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEPE("sortList/sortMethods.e");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEPE("sortList/sortNames.e");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEPE("sortList/sortTexts.e");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEPE("sortList/sortTimes.e");
		}

	}
}

