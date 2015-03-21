using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSortSet : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOPO("sortSet/sortBooleans.o");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOPO("sortSet/sortDates.o");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOPO("sortSet/sortDateTimes.o");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOPO("sortSet/sortDecimals.o");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOPO("sortSet/sortExpressions.o");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOPO("sortSet/sortIntegers.o");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOPO("sortSet/sortKeys.o");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOPO("sortSet/sortMethods.o");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOPO("sortSet/sortNames.o");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOPO("sortSet/sortTexts.o");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOPO("sortSet/sortTimes.o");
		}

	}
}

