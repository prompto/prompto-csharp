using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestContainer : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testHasAllFromList()
		{
			CheckOutput("container/hasAllFromList.poc");
		}

		[Test]
		public void testHasAllFromSet()
		{
			CheckOutput("container/hasAllFromSet.poc");
		}

		[Test]
		public void testHasAllList()
		{
			CheckOutput("container/hasAllList.poc");
		}

		[Test]
		public void testHasAllSet()
		{
			CheckOutput("container/hasAllSet.poc");
		}

		[Test]
		public void testHasAllText()
		{
			CheckOutput("container/hasAllText.poc");
		}

		[Test]
		public void testHasAllTuple()
		{
			CheckOutput("container/hasAllTuple.poc");
		}

		[Test]
		public void testHasAnyFromList()
		{
			CheckOutput("container/hasAnyFromList.poc");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			CheckOutput("container/hasAnyFromSet.poc");
		}

		[Test]
		public void testHasAnyList()
		{
			CheckOutput("container/hasAnyList.poc");
		}

		[Test]
		public void testHasAnySet()
		{
			CheckOutput("container/hasAnySet.poc");
		}

		[Test]
		public void testHasAnyText()
		{
			CheckOutput("container/hasAnyText.poc");
		}

		[Test]
		public void testHasAnyTuple()
		{
			CheckOutput("container/hasAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			CheckOutput("container/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			CheckOutput("container/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			CheckOutput("container/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			CheckOutput("container/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("container/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			CheckOutput("container/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			CheckOutput("container/inText.poc");
		}

		[Test]
		public void testInTextEnum()
		{
			CheckOutput("container/inTextEnum.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			CheckOutput("container/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			CheckOutput("container/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			CheckOutput("container/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			CheckOutput("container/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			CheckOutput("container/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			CheckOutput("container/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			CheckOutput("container/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			CheckOutput("container/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			CheckOutput("container/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			CheckOutput("container/ninTimeRange.poc");
		}

	}
}

