using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestSortList : BaseEParserTest
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
			CheckOutput("sortList/sortBooleans.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortList/sortDateTimes.pec");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortList/sortDates.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortList/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			CheckOutput("sortList/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			CheckOutput("sortList/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			CheckOutput("sortList/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			CheckOutput("sortList/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			CheckOutput("sortList/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			CheckOutput("sortList/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			CheckOutput("sortList/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			CheckOutput("sortList/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			CheckOutput("sortList/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			CheckOutput("sortList/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			CheckOutput("sortList/sortDescTimes.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortList/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortList/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortList/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortList/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortList/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortList/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortList/sortTimes.pec");
		}

	}
}

