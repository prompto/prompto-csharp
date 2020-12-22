using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestContainer : BaseOParserTest
	{

		[Test]
		public void testHasAllList()
		{
			compareResourceOMO("container/hasAllList.poc");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceOMO("container/hasAllSet.poc");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceOMO("container/hasAllText.poc");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceOMO("container/hasAllTuple.poc");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceOMO("container/hasAnyList.poc");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceOMO("container/hasAnySet.poc");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceOMO("container/hasAnyText.poc");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceOMO("container/hasAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOMO("container/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOMO("container/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOMO("container/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOMO("container/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			compareResourceOMO("container/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOMO("container/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			compareResourceOMO("container/inText.poc");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceOMO("container/inTextEnum.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOMO("container/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOMO("container/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOMO("container/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOMO("container/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOMO("container/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOMO("container/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOMO("container/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOMO("container/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOMO("container/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOMO("container/ninTimeRange.poc");
		}

	}
}

