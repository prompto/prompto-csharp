using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestContains : BaseOParserTest
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
		public void testHasAllList()
		{
			CheckOutput("contains/hasAllList.poc");
		}

		[Test]
		public void testHasAllSet()
		{
			CheckOutput("contains/hasAllSet.poc");
		}

		[Test]
		public void testHasAllText()
		{
			CheckOutput("contains/hasAllText.poc");
		}

		[Test]
		public void testHasAllTuple()
		{
			CheckOutput("contains/hasAllTuple.poc");
		}

		[Test]
		public void testHasAnyList()
		{
			CheckOutput("contains/hasAnyList.poc");
		}

		[Test]
		public void testHasAnySet()
		{
			CheckOutput("contains/hasAnySet.poc");
		}

		[Test]
		public void testHasAnyText()
		{
			CheckOutput("contains/hasAnyText.poc");
		}

		[Test]
		public void testHasAnyTuple()
		{
			CheckOutput("contains/hasAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			CheckOutput("contains/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			CheckOutput("contains/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			CheckOutput("contains/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			CheckOutput("contains/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("contains/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			CheckOutput("contains/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			CheckOutput("contains/inText.poc");
		}

		[Test]
		public void testInTextEnum()
		{
			CheckOutput("contains/inTextEnum.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			CheckOutput("contains/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			CheckOutput("contains/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			CheckOutput("contains/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			CheckOutput("contains/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			CheckOutput("contains/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			CheckOutput("contains/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			CheckOutput("contains/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			CheckOutput("contains/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			CheckOutput("contains/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			CheckOutput("contains/ninTimeRange.poc");
		}

	}
}

