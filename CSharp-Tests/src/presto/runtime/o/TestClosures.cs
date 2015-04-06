using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestClosures : BaseOParserTest
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
		public void testGlobalClosureNoArg()
		{
			CheckOutput("closures/globalClosureNoArg.poc");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			CheckOutput("closures/globalClosureWithArg.poc");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			CheckOutput("closures/instanceClosureNoArg.poc");
		}

	}
}

