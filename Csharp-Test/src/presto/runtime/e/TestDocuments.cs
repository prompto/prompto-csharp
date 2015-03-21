using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
			CheckOutput("documents/deepItem.e");
		}

		[Test]
		public void testDeepVariable()
		{
			CheckOutput("documents/deepVariable.e");
		}

		[Test]
		public void testItem()
		{
			CheckOutput("documents/item.e");
		}

		[Test]
		public void testVariable()
		{
			CheckOutput("documents/variable.e");
		}

	}
}

