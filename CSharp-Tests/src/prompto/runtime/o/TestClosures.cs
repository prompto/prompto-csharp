// generated: 2015-07-05T23:01:01.199
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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

