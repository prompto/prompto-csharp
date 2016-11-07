using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestFilter : BaseEParserTest
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
		public void testFilterFromList()
		{
			CheckOutput("filter/filterFromList.pec");
		}

		[Test]
		public void testFilterFromSet()
		{
			CheckOutput("filter/filterFromSet.pec");
		}

	}
}

