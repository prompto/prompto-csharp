using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("contains/containsAllList.pec");
		}

		[Test]
		public void testContainsAllSet()
		{
			CheckOutput("contains/containsAllSet.pec");
		}

		[Test]
		public void testContainsAllText()
		{
			CheckOutput("contains/containsAllText.pec");
		}

		[Test]
		public void testContainsAllTuple()
		{
			CheckOutput("contains/containsAllTuple.pec");
		}

		[Test]
		public void testContainsAnyList()
		{
			CheckOutput("contains/containsAnyList.pec");
		}

		[Test]
		public void testContainsAnySet()
		{
			CheckOutput("contains/containsAnySet.pec");
		}

		[Test]
		public void testContainsAnyText()
		{
			CheckOutput("contains/containsAnyText.pec");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			CheckOutput("contains/containsAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			CheckOutput("contains/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			CheckOutput("contains/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			CheckOutput("contains/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			CheckOutput("contains/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("contains/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			CheckOutput("contains/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			CheckOutput("contains/inText.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			CheckOutput("contains/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			CheckOutput("contains/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			CheckOutput("contains/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			CheckOutput("contains/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			CheckOutput("contains/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			CheckOutput("contains/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			CheckOutput("contains/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			CheckOutput("contains/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			CheckOutput("contains/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			CheckOutput("contains/ninTimeRange.pec");
		}

	}
}

