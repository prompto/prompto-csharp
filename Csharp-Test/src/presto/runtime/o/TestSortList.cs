using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testSortBooleans()
		{
			CheckOutput("sortList/sortBooleans.o");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortList/sortDates.o");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortList/sortDateTimes.o");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortList/sortDecimals.o");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortList/sortExpressions.o");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortList/sortIntegers.o");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortList/sortKeys.o");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortList/sortMethods.o");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortList/sortNames.o");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortList/sortTexts.o");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortList/sortTimes.o");
		}

	}
}

