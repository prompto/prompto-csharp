using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestSortSet : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceESE("sortSet/sortBooleans.pec");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceESE("sortSet/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceESE("sortSet/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceESE("sortSet/sortDecimals.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceESE("sortSet/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceESE("sortSet/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceESE("sortSet/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceESE("sortSet/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceESE("sortSet/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceESE("sortSet/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceESE("sortSet/sortTimes.pec");
		}

	}
}

