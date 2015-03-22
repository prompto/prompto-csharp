using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOEO("sortList/sortBooleans.o");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOEO("sortList/sortDates.o");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOEO("sortList/sortDateTimes.o");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOEO("sortList/sortDecimals.o");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOEO("sortList/sortExpressions.o");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOEO("sortList/sortIntegers.o");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOEO("sortList/sortKeys.o");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOEO("sortList/sortMethods.o");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOEO("sortList/sortNames.o");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOEO("sortList/sortTexts.o");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOEO("sortList/sortTimes.o");
		}

	}
}

