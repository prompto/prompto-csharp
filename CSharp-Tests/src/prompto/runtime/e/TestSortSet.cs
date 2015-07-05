// generated: 2015-07-05T23:01:01.423
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

