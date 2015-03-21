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
			CheckOutput("sortList/sortBooleans.e");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortList/sortDates.e");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortList/sortDateTimes.e");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortList/sortDecimals.e");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortList/sortExpressions.e");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortList/sortIntegers.e");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortList/sortKeys.e");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortList/sortMethods.e");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortList/sortNames.e");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortList/sortTexts.e");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortList/sortTimes.e");
		}

	}
}

