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
			CheckOutput("sortSet/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortSet/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortSet/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortSet/sortDecimals.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortSet/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortSet/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortSet/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortSet/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortSet/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortSet/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortSet/sortTimes.poc");
		}

	}
}

