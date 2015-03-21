using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOPO("sortList/sortBooleans.o");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOPO("sortList/sortDates.o");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOPO("sortList/sortDateTimes.o");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOPO("sortList/sortDecimals.o");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOPO("sortList/sortExpressions.o");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOPO("sortList/sortIntegers.o");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOPO("sortList/sortKeys.o");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOPO("sortList/sortMethods.o");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOPO("sortList/sortNames.o");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOPO("sortList/sortTexts.o");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOPO("sortList/sortTimes.o");
		}

	}
}

