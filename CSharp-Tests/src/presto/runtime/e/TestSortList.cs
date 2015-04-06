using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
		public void testSortDates()
		{
			CheckOutput("sortList/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortList/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortList/sortDecimals.pec");
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

