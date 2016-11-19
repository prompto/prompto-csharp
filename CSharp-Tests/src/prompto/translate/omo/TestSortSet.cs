using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSortSet : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOMO("sortSet/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOMO("sortSet/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOMO("sortSet/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOMO("sortSet/sortDecimals.poc");
		}

		[Test]
		public void testSortDescBooleans()
		{
			compareResourceOMO("sortSet/sortDescBooleans.poc");
		}

		[Test]
		public void testSortDescDates()
		{
			compareResourceOMO("sortSet/sortDescDates.poc");
		}

		[Test]
		public void testSortDescDateTimes()
		{
			compareResourceOMO("sortSet/sortDescDateTimes.poc");
		}

		[Test]
		public void testSortDescDecimals()
		{
			compareResourceOMO("sortSet/sortDescDecimals.poc");
		}

		[Test]
		public void testSortDescExpressions()
		{
			compareResourceOMO("sortSet/sortDescExpressions.poc");
		}

		[Test]
		public void testSortDescIntegers()
		{
			compareResourceOMO("sortSet/sortDescIntegers.poc");
		}

		[Test]
		public void testSortDescKeys()
		{
			compareResourceOMO("sortSet/sortDescKeys.poc");
		}

		[Test]
		public void testSortDescMethods()
		{
			compareResourceOMO("sortSet/sortDescMethods.poc");
		}

		[Test]
		public void testSortDescNames()
		{
			compareResourceOMO("sortSet/sortDescNames.poc");
		}

		[Test]
		public void testSortDescTexts()
		{
			compareResourceOMO("sortSet/sortDescTexts.poc");
		}

		[Test]
		public void testSortDescTimes()
		{
			compareResourceOMO("sortSet/sortDescTimes.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOMO("sortSet/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOMO("sortSet/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOMO("sortSet/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOMO("sortSet/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOMO("sortSet/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOMO("sortSet/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOMO("sortSet/sortTimes.poc");
		}

	}
}

