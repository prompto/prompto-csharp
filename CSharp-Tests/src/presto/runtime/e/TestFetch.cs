using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestFetch : BaseEParserTest
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
		public void testFetchFromList()
		{
			CheckOutput("fetch/fetchFromList.pec");
		}

		[Test]
		public void testFetchFromSet()
		{
			CheckOutput("fetch/fetchFromSet.pec");
		}

	}
}

