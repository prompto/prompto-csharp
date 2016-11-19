using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestContains : BaseEParserTest
	{

		[Test]
		public void testContainsAllDict()
		{
			compareResourceEME("contains/containsAllDict.pec");
		}

		[Test]
		public void testContainsAllList()
		{
			compareResourceEME("contains/containsAllList.pec");
		}

		[Test]
		public void testContainsAllRange()
		{
			compareResourceEME("contains/containsAllRange.pec");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceEME("contains/containsAllSet.pec");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceEME("contains/containsAllText.pec");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceEME("contains/containsAllTuple.pec");
		}

		[Test]
		public void testContainsAnyDict()
		{
			compareResourceEME("contains/containsAnyDict.pec");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceEME("contains/containsAnyList.pec");
		}

		[Test]
		public void testContainsAnyRange()
		{
			compareResourceEME("contains/containsAnyRange.pec");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceEME("contains/containsAnySet.pec");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceEME("contains/containsAnyText.pec");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceEME("contains/containsAnyTuple.pec");
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

