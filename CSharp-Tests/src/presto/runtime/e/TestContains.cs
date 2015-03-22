using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestContains : BaseEParserTest
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
			CheckOutput("contains/containsAllList.e");
		}

		[Test]
		public void testContainsAllSet()
		{
			CheckOutput("contains/containsAllSet.e");
		}

		[Test]
		public void testContainsAllText()
		{
			CheckOutput("contains/containsAllText.e");
		}

		[Test]
		public void testContainsAllTuple()
		{
			CheckOutput("contains/containsAllTuple.e");
		}

		[Test]
		public void testContainsAnyList()
		{
			CheckOutput("contains/containsAnyList.e");
		}

		[Test]
		public void testContainsAnySet()
		{
			CheckOutput("contains/containsAnySet.e");
		}

		[Test]
		public void testContainsAnyText()
		{
			CheckOutput("contains/containsAnyText.e");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			CheckOutput("contains/containsAnyTuple.e");
		}

		[Test]
		public void testInCharacterRange()
		{
			CheckOutput("contains/inCharacterRange.e");
		}

		[Test]
		public void testInDateRange()
		{
			CheckOutput("contains/inDateRange.e");
		}

		[Test]
		public void testInDict()
		{
			CheckOutput("contains/inDict.e");
		}

		[Test]
		public void testInIntegerRange()
		{
			CheckOutput("contains/inIntegerRange.e");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("contains/inList.e");
		}

		[Test]
		public void testInSet()
		{
			CheckOutput("contains/inSet.e");
		}

		[Test]
		public void testInText()
		{
			CheckOutput("contains/inText.e");
		}

		[Test]
		public void testInTimeRange()
		{
			CheckOutput("contains/inTimeRange.e");
		}

		[Test]
		public void testInTuple()
		{
			CheckOutput("contains/inTuple.e");
		}

		[Test]
		public void testNinCharacterRange()
		{
			CheckOutput("contains/ninCharacterRange.e");
		}

		[Test]
		public void testNinDateRange()
		{
			CheckOutput("contains/ninDateRange.e");
		}

		[Test]
		public void testNinDict()
		{
			CheckOutput("contains/ninDict.e");
		}

		[Test]
		public void testNinIntegerRange()
		{
			CheckOutput("contains/ninIntegerRange.e");
		}

		[Test]
		public void testNinList()
		{
			CheckOutput("contains/ninList.e");
		}

		[Test]
		public void testNinSet()
		{
			CheckOutput("contains/ninSet.e");
		}

		[Test]
		public void testNinText()
		{
			CheckOutput("contains/ninText.e");
		}

		[Test]
		public void testNinTimeRange()
		{
			CheckOutput("contains/ninTimeRange.e");
		}

	}
}

