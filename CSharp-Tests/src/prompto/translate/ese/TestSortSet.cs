using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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
		public void testSortDescBooleans()
		{
			compareResourceESE("sortSet/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceESE("sortSet/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceESE("sortSet/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceESE("sortSet/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceESE("sortSet/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceESE("sortSet/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceESE("sortSet/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceESE("sortSet/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceESE("sortSet/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceESE("sortSet/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceESE("sortSet/sortDescTimes.pec");
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

