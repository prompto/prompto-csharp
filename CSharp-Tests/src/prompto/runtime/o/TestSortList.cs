using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
		public void testSortDateTimes()
		{
			CheckOutput("sortList/sortDateTimes.poc");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortList/sortDates.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortList/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			CheckOutput("sortList/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			CheckOutput("sortList/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			CheckOutput("sortList/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			CheckOutput("sortList/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			CheckOutput("sortList/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			CheckOutput("sortList/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			CheckOutput("sortList/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			CheckOutput("sortList/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			CheckOutput("sortList/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			CheckOutput("sortList/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			CheckOutput("sortList/sortDescTimes.poc");
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

