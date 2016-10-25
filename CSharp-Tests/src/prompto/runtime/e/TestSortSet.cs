using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("sortSet/sortBooleans.pec");
		}

		[Test]
		public void testSortDates()
		{
			CheckOutput("sortSet/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			CheckOutput("sortSet/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			CheckOutput("sortSet/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			CheckOutput("sortSet/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			CheckOutput("sortSet/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			CheckOutput("sortSet/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			CheckOutput("sortSet/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			CheckOutput("sortSet/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			CheckOutput("sortSet/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			CheckOutput("sortSet/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			CheckOutput("sortSet/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			CheckOutput("sortSet/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			CheckOutput("sortSet/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			CheckOutput("sortSet/sortDescTimes.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			CheckOutput("sortSet/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			CheckOutput("sortSet/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			CheckOutput("sortSet/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			CheckOutput("sortSet/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			CheckOutput("sortSet/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			CheckOutput("sortSet/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			CheckOutput("sortSet/sortTimes.pec");
		}

	}
}

