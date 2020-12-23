using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestContainer : BaseOParserTest
	{

		[Test]
		public void testHasAllFromList()
		{
			compareResourceOEO("container/hasAllFromList.poc");
		}

		[Test]
		public void testHasAllFromSet()
		{
			compareResourceOEO("container/hasAllFromSet.poc");
		}

		[Test]
		public void testHasAllList()
		{
			compareResourceOEO("container/hasAllList.poc");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceOEO("container/hasAllSet.poc");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceOEO("container/hasAllText.poc");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceOEO("container/hasAllTuple.poc");
		}

		[Test]
		public void testHasAnyFromList()
		{
			compareResourceOEO("container/hasAnyFromList.poc");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			compareResourceOEO("container/hasAnyFromSet.poc");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceOEO("container/hasAnyList.poc");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceOEO("container/hasAnySet.poc");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceOEO("container/hasAnyText.poc");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceOEO("container/hasAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOEO("container/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOEO("container/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOEO("container/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOEO("container/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			compareResourceOEO("container/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOEO("container/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			compareResourceOEO("container/inText.poc");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceOEO("container/inTextEnum.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOEO("container/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOEO("container/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOEO("container/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOEO("container/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOEO("container/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOEO("container/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOEO("container/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOEO("container/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOEO("container/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOEO("container/ninTimeRange.poc");
		}

	}
}

