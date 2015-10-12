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

