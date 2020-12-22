using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestContains : BaseEParserTest
	{

		[Test]
		public void testHasAllDict()
		{
			compareResourceEOE("contains/hasAllDict.pec");
		}

		[Test]
		public void testHasAllList()
		{
			compareResourceEOE("contains/hasAllList.pec");
		}

		[Test]
		public void testHasAllRange()
		{
			compareResourceEOE("contains/hasAllRange.pec");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceEOE("contains/hasAllSet.pec");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceEOE("contains/hasAllText.pec");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceEOE("contains/hasAllTuple.pec");
		}

		[Test]
		public void testHasAnyDict()
		{
			compareResourceEOE("contains/hasAnyDict.pec");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceEOE("contains/hasAnyList.pec");
		}

		[Test]
		public void testHasAnyRange()
		{
			compareResourceEOE("contains/hasAnyRange.pec");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceEOE("contains/hasAnySet.pec");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceEOE("contains/hasAnyText.pec");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceEOE("contains/hasAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEOE("contains/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEOE("contains/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEOE("contains/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEOE("contains/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEOE("contains/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEOE("contains/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			compareResourceEOE("contains/inText.pec");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceEOE("contains/inTextEnum.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEOE("contains/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEOE("contains/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEOE("contains/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEOE("contains/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEOE("contains/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEOE("contains/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEOE("contains/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEOE("contains/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEOE("contains/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEOE("contains/ninTimeRange.pec");
		}

	}
}

