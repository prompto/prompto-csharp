// generated: 2015-10-05T01:03:20.469
using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

	[TestFixture]
	public class TestInternet : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Loader.Load ();
			Out.init();
			coreContext = null;
			LoadDependency("internet");
			LoadDependency("console");
			LoadDependency("core");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testUrl()
		{
			CheckTests("internet/url.pec");
		}

	}
}

