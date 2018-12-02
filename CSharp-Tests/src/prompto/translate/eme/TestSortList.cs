using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSortList : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceEME("sortList/sortBooleans.pec");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEME("sortList/sortDateTimes.pec");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEME("sortList/sortDates.pec");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEME("sortList/sortDecimals.pec");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceEME("sortList/sortDescBooleans.pec");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceEME("sortList/sortDescDateTimes.pec");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceEME("sortList/sortDescDates.pec");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceEME("sortList/sortDescDecimals.pec");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceEME("sortList/sortDescExpressions.pec");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceEME("sortList/sortDescIntegers.pec");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceEME("sortList/sortDescKeys.pec");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceEME("sortList/sortDescMethods.pec");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceEME("sortList/sortDescNames.pec");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceEME("sortList/sortDescTexts.pec");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceEME("sortList/sortDescTimes.pec");
		}

		[Test]
		public void testSortDocumentExpressions()
		{
			compareResourceEME("sortList/sortDocumentExpressions.pec");
		}

		[Test]
		public void testSortDocumentKeys()
		{
			compareResourceEME("sortList/sortDocumentKeys.pec");
		}

		[Test]
		public void testSortDocumentMethods()
		{
			compareResourceEME("sortList/sortDocumentMethods.pec");
		}

		[Test]
		public void testSortDocumentNames()
		{
			compareResourceEME("sortList/sortDocumentNames.pec");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEME("sortList/sortExpressions.pec");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEME("sortList/sortIntegers.pec");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEME("sortList/sortKeys.pec");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEME("sortList/sortMethods.pec");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEME("sortList/sortNames.pec");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEME("sortList/sortTexts.pec");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEME("sortList/sortTimes.pec");
		}

	}
}

