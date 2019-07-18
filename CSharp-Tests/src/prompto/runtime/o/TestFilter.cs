using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestFilter : BaseOParserTest
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
		public void testFilterFromIterable()
		{
			CheckOutput("filter/filterFromIterable.poc");
		}

		[Test]
		public void testFilterFromList()
		{
			CheckOutput("filter/filterFromList.poc");
		}

		[Test]
		public void testFilterFromSet()
		{
			CheckOutput("filter/filterFromSet.poc");
		}

	}
}

