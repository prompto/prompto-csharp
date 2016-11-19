using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSortSet : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceEME("sortSet/sortBooleans.pec");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEME("sortSet/sortDates.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEME("sortSet/sortDateTimes.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEME("sortSet/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceEME("sortSet/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceEME("sortSet/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceEME("sortSet/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceEME("sortSet/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceEME("sortSet/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceEME("sortSet/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceEME("sortSet/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceEME("sortSet/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceEME("sortSet/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceEME("sortSet/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceEME("sortSet/sortDescTimes.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEME("sortSet/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEME("sortSet/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEME("sortSet/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEME("sortSet/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEME("sortSet/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEME("sortSet/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEME("sortSet/sortTimes.pec");
		}

	}
}

