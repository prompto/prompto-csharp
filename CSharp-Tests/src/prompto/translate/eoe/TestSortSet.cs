using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSortSet : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceEOE("sortSet/sortBooleans.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEOE("sortSet/sortDateTimes.pec");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEOE("sortSet/sortDates.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEOE("sortSet/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceEOE("sortSet/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceEOE("sortSet/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceEOE("sortSet/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceEOE("sortSet/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceEOE("sortSet/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceEOE("sortSet/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceEOE("sortSet/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceEOE("sortSet/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceEOE("sortSet/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceEOE("sortSet/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceEOE("sortSet/sortDescTimes.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEOE("sortSet/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEOE("sortSet/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEOE("sortSet/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEOE("sortSet/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEOE("sortSet/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEOE("sortSet/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEOE("sortSet/sortTimes.pec");
		}

	}
}

