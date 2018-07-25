using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

	[TestFixture]
	public class TestWeb : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Loader.Load ();
			Out.init();
			coreContext = null;
			LoadDependency("web");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testDom()
		{
			CheckTests("web/Dom.pec");
		}

	}
}

