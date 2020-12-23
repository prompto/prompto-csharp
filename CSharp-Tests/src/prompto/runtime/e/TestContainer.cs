using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestContainer : BaseEParserTest
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
			CheckOutput("container/hasAllDict.pec");
		}

		[Test]
		public void testHasAllFromList()
		{
			CheckOutput("container/hasAllFromList.pec");
		}

		[Test]
		public void testHasAllFromSet()
		{
			CheckOutput("container/hasAllFromSet.pec");
		}

		[Test]
		public void testHasAllList()
		{
			CheckOutput("container/hasAllList.pec");
		}

		[Test]
		public void testHasAllRange()
		{
			CheckOutput("container/hasAllRange.pec");
		}

		[Test]
		public void testHasAllSet()
		{
			CheckOutput("container/hasAllSet.pec");
		}

		[Test]
		public void testHasAllText()
		{
			CheckOutput("container/hasAllText.pec");
		}

		[Test]
		public void testHasAllTuple()
		{
			CheckOutput("container/hasAllTuple.pec");
		}

		[Test]
		public void testHasAnyDict()
		{
			CheckOutput("container/hasAnyDict.pec");
		}

		[Test]
		public void testHasAnyFromList()
		{
			CheckOutput("container/hasAnyFromList.pec");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			CheckOutput("container/hasAnyFromSet.pec");
		}

		[Test]
		public void testHasAnyList()
		{
			CheckOutput("container/hasAnyList.pec");
		}

		[Test]
		public void testHasAnyRange()
		{
			CheckOutput("container/hasAnyRange.pec");
		}

		[Test]
		public void testHasAnySet()
		{
			CheckOutput("container/hasAnySet.pec");
		}

		[Test]
		public void testHasAnyText()
		{
			CheckOutput("container/hasAnyText.pec");
		}

		[Test]
		public void testHasAnyTuple()
		{
			CheckOutput("container/hasAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			CheckOutput("container/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			CheckOutput("container/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			CheckOutput("container/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			CheckOutput("container/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("container/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			CheckOutput("container/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			CheckOutput("container/inText.pec");
		}

		[Test]
		public void testInTextEnum()
		{
			CheckOutput("container/inTextEnum.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			CheckOutput("container/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			CheckOutput("container/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			CheckOutput("container/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			CheckOutput("container/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			CheckOutput("container/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			CheckOutput("container/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			CheckOutput("container/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			CheckOutput("container/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			CheckOutput("container/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			CheckOutput("container/ninTimeRange.pec");
		}

	}
}

