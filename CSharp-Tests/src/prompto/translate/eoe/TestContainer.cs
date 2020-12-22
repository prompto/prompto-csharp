using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestContainer : BaseEParserTest
	{

		[Test]
		public void testHasAllDict()
		{
			compareResourceEOE("container/hasAllDict.pec");
		}

		[Test]
		public void testHasAllList()
		{
			compareResourceEOE("container/hasAllList.pec");
		}

		[Test]
		public void testHasAllRange()
		{
			compareResourceEOE("container/hasAllRange.pec");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceEOE("container/hasAllSet.pec");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceEOE("container/hasAllText.pec");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceEOE("container/hasAllTuple.pec");
		}

		[Test]
		public void testHasAnyDict()
		{
			compareResourceEOE("container/hasAnyDict.pec");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceEOE("container/hasAnyList.pec");
		}

		[Test]
		public void testHasAnyRange()
		{
			compareResourceEOE("container/hasAnyRange.pec");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceEOE("container/hasAnySet.pec");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceEOE("container/hasAnyText.pec");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceEOE("container/hasAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEOE("container/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEOE("container/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEOE("container/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEOE("container/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEOE("container/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEOE("container/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			compareResourceEOE("container/inText.pec");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceEOE("container/inTextEnum.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEOE("container/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEOE("container/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEOE("container/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEOE("container/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEOE("container/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEOE("container/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEOE("container/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEOE("container/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEOE("container/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEOE("container/ninTimeRange.pec");
		}

	}
}

