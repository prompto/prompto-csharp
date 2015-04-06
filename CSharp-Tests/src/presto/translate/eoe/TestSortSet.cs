using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
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
		public void testSortDates()
		{
			compareResourceEOE("sortSet/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEOE("sortSet/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEOE("sortSet/sortDecimals.pec");
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

