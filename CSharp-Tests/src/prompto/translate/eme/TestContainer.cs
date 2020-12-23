using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestContainer : BaseEParserTest
	{

		[Test]
		public void testHasAllDict()
		{
			compareResourceEME("container/hasAllDict.pec");
		}

		[Test]
		public void testHasAllFromList()
		{
			compareResourceEME("container/hasAllFromList.pec");
		}

		[Test]
		public void testHasAllFromSet()
		{
			compareResourceEME("container/hasAllFromSet.pec");
		}

		[Test]
		public void testHasAllList()
		{
			compareResourceEME("container/hasAllList.pec");
		}

		[Test]
		public void testHasAllRange()
		{
			compareResourceEME("container/hasAllRange.pec");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceEME("container/hasAllSet.pec");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceEME("container/hasAllText.pec");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceEME("container/hasAllTuple.pec");
		}

		[Test]
		public void testHasAnyDict()
		{
			compareResourceEME("container/hasAnyDict.pec");
		}

		[Test]
		public void testHasAnyFromList()
		{
			compareResourceEME("container/hasAnyFromList.pec");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			compareResourceEME("container/hasAnyFromSet.pec");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceEME("container/hasAnyList.pec");
		}

		[Test]
		public void testHasAnyRange()
		{
			compareResourceEME("container/hasAnyRange.pec");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceEME("container/hasAnySet.pec");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceEME("container/hasAnyText.pec");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceEME("container/hasAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEME("container/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEME("container/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEME("container/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEME("container/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEME("container/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEME("container/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			compareResourceEME("container/inText.pec");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceEME("container/inTextEnum.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEME("container/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEME("container/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEME("container/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEME("container/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEME("container/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEME("container/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEME("container/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEME("container/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEME("container/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEME("container/ninTimeRange.pec");
		}

	}
}

