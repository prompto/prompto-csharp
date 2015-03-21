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
			compareResourceEOE("sortSet/sortBooleans.e");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEOE("sortSet/sortDates.e");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEOE("sortSet/sortDateTimes.e");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEOE("sortSet/sortDecimals.e");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEOE("sortSet/sortExpressions.e");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEOE("sortSet/sortIntegers.e");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEOE("sortSet/sortKeys.e");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEOE("sortSet/sortMethods.e");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEOE("sortSet/sortNames.e");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEOE("sortSet/sortTexts.e");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEOE("sortSet/sortTimes.e");
		}

	}
}

