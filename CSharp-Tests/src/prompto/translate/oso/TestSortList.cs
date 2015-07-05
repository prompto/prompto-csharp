// generated: 2015-07-05T23:01:01.418
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestSortList : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOSO("sortList/sortBooleans.poc");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOSO("sortList/sortDates.poc");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOSO("sortList/sortDateTimes.poc");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOSO("sortList/sortDecimals.poc");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOSO("sortList/sortExpressions.poc");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOSO("sortList/sortIntegers.poc");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOSO("sortList/sortKeys.poc");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOSO("sortList/sortMethods.poc");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOSO("sortList/sortNames.poc");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOSO("sortList/sortTexts.poc");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOSO("sortList/sortTimes.poc");
		}

	}
}

