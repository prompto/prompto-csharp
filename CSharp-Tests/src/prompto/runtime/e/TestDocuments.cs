using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
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
		public void testDeepItem()
		{
			CheckOutput("documents/deepItem.pec");
		}

		[Test]
		public void testDeepVariable()
		{
			CheckOutput("documents/deepVariable.pec");
		}

		[Test]
		public void testItem()
		{
			CheckOutput("documents/item.pec");
		}

		[Test]
		public void testVariable()
		{
			CheckOutput("documents/variable.pec");
		}

	}
}

