using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestSortSet : BaseOParserTest
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
			CheckOutput("sortSet/sortBooleans.o");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortSet/sortDates.o");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortSet/sortDateTimes.o");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortSet/sortDecimals.o");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortSet/sortExpressions.o");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortSet/sortIntegers.o");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortSet/sortKeys.o");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortSet/sortMethods.o");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortSet/sortNames.o");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortSet/sortTexts.o");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortSet/sortTimes.o");
		}

	}
}

