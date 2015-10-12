using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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

