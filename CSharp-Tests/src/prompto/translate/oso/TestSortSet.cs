using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestSortSet : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOSO("sortSet/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOSO("sortSet/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOSO("sortSet/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOSO("sortSet/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceOSO("sortSet/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceOSO("sortSet/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceOSO("sortSet/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceOSO("sortSet/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceOSO("sortSet/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceOSO("sortSet/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceOSO("sortSet/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceOSO("sortSet/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceOSO("sortSet/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceOSO("sortSet/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceOSO("sortSet/sortDescTimes.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOSO("sortSet/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOSO("sortSet/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOSO("sortSet/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOSO("sortSet/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOSO("sortSet/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOSO("sortSet/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOSO("sortSet/sortTimes.poc");
		}

	}
}

