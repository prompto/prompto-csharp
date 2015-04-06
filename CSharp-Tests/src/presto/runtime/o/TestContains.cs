using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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
		public void testContainsAllList()
		{
			CheckOutput("contains/containsAllList.poc");
		}

		[Test]
		public void testContainsAllSet()
		{
			CheckOutput("contains/containsAllSet.poc");
		}

		[Test]
		public void testContainsAllText()
		{
			CheckOutput("contains/containsAllText.poc");
		}

		[Test]
		public void testContainsAllTuple()
		{
			CheckOutput("contains/containsAllTuple.poc");
		}

		[Test]
		public void testContainsAnyList()
		{
			CheckOutput("contains/containsAnyList.poc");
		}

		[Test]
		public void testContainsAnySet()
		{
			CheckOutput("contains/containsAnySet.poc");
		}

		[Test]
		public void testContainsAnyText()
		{
			CheckOutput("contains/containsAnyText.poc");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			CheckOutput("contains/containsAnyTuple.poc");
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

