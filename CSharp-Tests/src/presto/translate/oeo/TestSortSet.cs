using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSortSet : BaseOParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceOEO("sortSet/sortBooleans.o");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceOEO("sortSet/sortDates.o");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceOEO("sortSet/sortDateTimes.o");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceOEO("sortSet/sortDecimals.o");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceOEO("sortSet/sortExpressions.o");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceOEO("sortSet/sortIntegers.o");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceOEO("sortSet/sortKeys.o");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceOEO("sortSet/sortMethods.o");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceOEO("sortSet/sortNames.o");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceOEO("sortSet/sortTexts.o");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceOEO("sortSet/sortTimes.o");
		}

	}
}

