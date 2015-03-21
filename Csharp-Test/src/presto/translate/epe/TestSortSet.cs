using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSortSet : BaseEParserTest
	{

		[Test]
		public void testSortBooleans()
		{
			compareResourceEPE("sortSet/sortBooleans.e");
		}

		[Test]
		public void testSortDates()
		{
			compareResourceEPE("sortSet/sortDates.e");
		}

		[Test]
		public void testSortDateTimes()
		{
			compareResourceEPE("sortSet/sortDateTimes.e");
		}

		[Test]
		public void testSortDecimals()
		{
			compareResourceEPE("sortSet/sortDecimals.e");
		}

		[Test]
		public void testSortExpressions()
		{
			compareResourceEPE("sortSet/sortExpressions.e");
		}

		[Test]
		public void testSortIntegers()
		{
			compareResourceEPE("sortSet/sortIntegers.e");
		}

		[Test]
		public void testSortKeys()
		{
			compareResourceEPE("sortSet/sortKeys.e");
		}

		[Test]
		public void testSortMethods()
		{
			compareResourceEPE("sortSet/sortMethods.e");
		}

		[Test]
		public void testSortNames()
		{
			compareResourceEPE("sortSet/sortNames.e");
		}

		[Test]
		public void testSortTexts()
		{
			compareResourceEPE("sortSet/sortTexts.e");
		}

		[Test]
		public void testSortTimes()
		{
			compareResourceEPE("sortSet/sortTimes.e");
		}

	}
}

