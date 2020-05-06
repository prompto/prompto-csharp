using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

	[TestFixture]
	public class TestReader : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Loader.Load ();
			Out.init();
			coreContext = null;
			LoadDependency("reader");
			LoadDependency("core");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testCsv()
		{
			CheckTests("reader/csv.pec");
		}

		[Test]
		public void testJson()
		{
			CheckTests("reader/json.pec");
		}

		[Test]
		public void testYaml()
		{
			CheckTests("reader/yaml.pec");
		}

	}
}

