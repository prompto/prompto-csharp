using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

	[TestFixture]
	public class TestSystem : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Loader.Load ();
			Out.init();
			coreContext = null;
			LoadDependency("system");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testSystem()
		{
			CheckTests("system/system.pec");
		}

	}
}

