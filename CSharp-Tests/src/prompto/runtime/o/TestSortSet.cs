using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
		public void testSortDateTimes()
		{
			CheckOutput("sortSet/sortDateTimes.poc");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortSet/sortDates.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortSet/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			CheckOutput("sortSet/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			CheckOutput("sortSet/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			CheckOutput("sortSet/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			CheckOutput("sortSet/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			CheckOutput("sortSet/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			CheckOutput("sortSet/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			CheckOutput("sortSet/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			CheckOutput("sortSet/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			CheckOutput("sortSet/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			CheckOutput("sortSet/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			CheckOutput("sortSet/sortDescTimes.poc");
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

