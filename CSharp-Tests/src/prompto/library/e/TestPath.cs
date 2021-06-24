using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

	[TestFixture]
	public class TestPath : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Loader.Load ();
			Out.init();
			coreContext = null;
			LoadDependency("path");
			LoadDependency("core");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testPath()
		{
			CheckTests("path/path.pec");
		}

	}
}

