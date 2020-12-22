using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestContains : BaseEParserTest
	{

		[Test]
		public void testHasAllDict()
		{
			compareResourceEME("contains/hasAllDict.pec");
		}

		[Test]
		public void testHasAllList()
		{
			compareResourceEME("contains/hasAllList.pec");
		}

		[Test]
		public void testHasAllRange()
		{
			compareResourceEME("contains/hasAllRange.pec");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceEME("contains/hasAllSet.pec");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceEME("contains/hasAllText.pec");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceEME("contains/hasAllTuple.pec");
		}

		[Test]
		public void testHasAnyDict()
		{
			compareResourceEME("contains/hasAnyDict.pec");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceEME("contains/hasAnyList.pec");
		}

		[Test]
		public void testHasAnyRange()
		{
			compareResourceEME("contains/hasAnyRange.pec");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceEME("contains/hasAnySet.pec");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceEME("contains/hasAnyText.pec");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceEME("contains/hasAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEME("contains/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEME("contains/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEME("contains/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEME("contains/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEME("contains/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEME("contains/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			compareResourceEME("contains/inText.pec");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceEME("contains/inTextEnum.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEME("contains/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEME("contains/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEME("contains/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEME("contains/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEME("contains/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEME("contains/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEME("contains/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEME("contains/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEME("contains/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEME("contains/ninTimeRange.pec");
		}

	}
}

