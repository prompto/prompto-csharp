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
		public void testHasAllDict()
		{
			CheckOutput("contains/hasAllDict.pec");
		}

		[Test]
		public void testHasAllList()
		{
			CheckOutput("contains/hasAllList.pec");
		}

		[Test]
		public void testHasAllRange()
		{
			CheckOutput("contains/hasAllRange.pec");
		}

		[Test]
		public void testHasAllSet()
		{
			CheckOutput("contains/hasAllSet.pec");
		}

		[Test]
		public void testHasAllText()
		{
			CheckOutput("contains/hasAllText.pec");
		}

		[Test]
		public void testHasAllTuple()
		{
			CheckOutput("contains/hasAllTuple.pec");
		}

		[Test]
		public void testHasAnyDict()
		{
			CheckOutput("contains/hasAnyDict.pec");
		}

		[Test]
		public void testHasAnyList()
		{
			CheckOutput("contains/hasAnyList.pec");
		}

		[Test]
		public void testHasAnyRange()
		{
			CheckOutput("contains/hasAnyRange.pec");
		}

		[Test]
		public void testHasAnySet()
		{
			CheckOutput("contains/hasAnySet.pec");
		}

		[Test]
		public void testHasAnyText()
		{
			CheckOutput("contains/hasAnyText.pec");
		}

		[Test]
		public void testHasAnyTuple()
		{
			CheckOutput("contains/hasAnyTuple.pec");
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
		public void testInTextEnum()
		{
			CheckOutput("contains/inTextEnum.pec");
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

