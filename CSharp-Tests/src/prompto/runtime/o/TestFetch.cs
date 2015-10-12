using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestFetch : BaseOParserTest
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
			CheckOutput("fetch/fetchFromList.poc");
		}

		[Test]
		public void testFetchFromSet()
		{
			CheckOutput("fetch/fetchFromSet.poc");
		}

	}
}

