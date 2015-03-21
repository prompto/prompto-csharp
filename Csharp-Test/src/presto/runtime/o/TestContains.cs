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
			CheckOutput("contains/containsAllList.o");
		}

		[Test]
		public void testContainsAllSet()
		{
			CheckOutput("contains/containsAllSet.o");
		}

		[Test]
		public void testContainsAllText()
		{
			CheckOutput("contains/containsAllText.o");
		}

		[Test]
		public void testContainsAllTuple()
		{
			CheckOutput("contains/containsAllTuple.o");
		}

		[Test]
		public void testContainsAnyList()
		{
			CheckOutput("contains/containsAnyList.o");
		}

		[Test]
		public void testContainsAnySet()
		{
			CheckOutput("contains/containsAnySet.o");
		}

		[Test]
		public void testContainsAnyText()
		{
			CheckOutput("contains/containsAnyText.o");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			CheckOutput("contains/containsAnyTuple.o");
		}

		[Test]
		public void testInCharacterRange()
		{
			CheckOutput("contains/inCharacterRange.o");
		}

		[Test]
		public void testInDateRange()
		{
			CheckOutput("contains/inDateRange.o");
		}

		[Test]
		public void testInDict()
		{
			CheckOutput("contains/inDict.o");
		}

		[Test]
		public void testInIntegerRange()
		{
			CheckOutput("contains/inIntegerRange.o");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("contains/inList.o");
		}

		[Test]
		public void testInSet()
		{
			CheckOutput("contains/inSet.o");
		}

		[Test]
		public void testInText()
		{
			CheckOutput("contains/inText.o");
		}

		[Test]
		public void testInTimeRange()
		{
			CheckOutput("contains/inTimeRange.o");
		}

		[Test]
		public void testInTuple()
		{
			CheckOutput("contains/inTuple.o");
		}

		[Test]
		public void testNinCharacterRange()
		{
			CheckOutput("contains/ninCharacterRange.o");
		}

		[Test]
		public void testNinDateRange()
		{
			CheckOutput("contains/ninDateRange.o");
		}

		[Test]
		public void testNinDict()
		{
			CheckOutput("contains/ninDict.o");
		}

		[Test]
		public void testNinIntegerRange()
		{
			CheckOutput("contains/ninIntegerRange.o");
		}

		[Test]
		public void testNinList()
		{
			CheckOutput("contains/ninList.o");
		}

		[Test]
		public void testNinSet()
		{
			CheckOutput("contains/ninSet.o");
		}

		[Test]
		public void testNinText()
		{
			CheckOutput("contains/ninText.o");
		}

		[Test]
		public void testNinTimeRange()
		{
			CheckOutput("contains/ninTimeRange.o");
		}

	}
}

