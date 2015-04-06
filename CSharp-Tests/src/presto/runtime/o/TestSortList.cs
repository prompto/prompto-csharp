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
			CheckOutput("sortList/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortList/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortList/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortList/sortDecimals.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortList/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortList/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortList/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortList/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortList/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortList/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortList/sortTimes.poc");
		}

	}
}

