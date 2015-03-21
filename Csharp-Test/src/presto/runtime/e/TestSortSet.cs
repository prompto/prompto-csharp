using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestSortSet : BaseEParserTest
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
			CheckOutput("sortSet/sortBooleans.e");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortSet/sortDates.e");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortSet/sortDateTimes.e");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortSet/sortDecimals.e");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortSet/sortExpressions.e");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortSet/sortIntegers.e");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortSet/sortKeys.e");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortSet/sortMethods.e");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortSet/sortNames.e");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortSet/sortTexts.e");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortSet/sortTimes.e");
		}

	}
}

