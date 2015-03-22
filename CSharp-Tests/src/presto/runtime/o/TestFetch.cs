using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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
			CheckOutput("fetch/fetchFromList.o");
		}

		[Test]
		public void testFetchFromSet()
		{
			CheckOutput("fetch/fetchFromSet.o");
		}

	}
}

