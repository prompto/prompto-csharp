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
			LoadDependency("core");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testEvents()
		{
			CheckTests("web/events.pec");
		}

		[Test]
		public void testFileRef()
		{
			CheckTests("web/fileRef.pec");
		}

		[Test]
		public void testReact()
		{
			CheckTests("web/react.pec");
		}

		[Test]
		public void testUtils()
		{
			CheckTests("web/utils.pec");
		}

		[Test]
		public void testWebSocket()
		{
			CheckTests("web/webSocket.pec");
		}

	}
}

