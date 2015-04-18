using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestClosures : BaseEParserTest
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
			CheckOutput("closures/globalClosureNoArg.pec");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			CheckOutput("closures/globalClosureWithArg.pec");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			CheckOutput("closures/instanceClosureNoArg.pec");
		}

	}
}
